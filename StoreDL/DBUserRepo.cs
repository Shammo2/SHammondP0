using Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Serilog;
namespace StoreDL;

public class DBUserRepo{
    
    private string _connectionString;

public DBUserRepo(string connectionString){
    _connectionString = connectionString;
    _connectionString = File.ReadAllText("connectionString.txt");
}

public List<Customer> GetAllUsers(){
    List<Customer> GetAllCustomers = new List<Customer>();
    using SqlConnection connection = new SqlConnection(_connectionString);
    
    string UserSelect = "Select * From Customer";
    string storeOrderSelect = "Select * From StoreOrder";
    string CustomerOrderSelect = "Select * From CustomerOrder";
    DataSet UserSet = new DataSet();
    
    //adapters for the tables
    using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);
    using SqlDataAdapter CustomerOrderAdapter = new SqlDataAdapter(CustomerOrderSelect, connection);
    using SqlDataAdapter CustomerAdapter = new SqlDataAdapter(UserSelect, connection);
    
    //filling each table using the adapters
    storeOrderAdapter.Fill(UserSet,"StoreOrder");
    CustomerOrderAdapter.Fill(UserSet,"CustomerOrder");
    CustomerAdapter.Fill(UserSet, "Customer");
    
    DataTable customerTable = UserSet.Tables["Customer"];
    DataTable CustomerOrderTable = UserSet.Tables["CustomerOrder"];
    DataTable StoreOrderTable = UserSet.Tables["StoreOrder"];
        if(customerTable!= null){   
            foreach(DataRow row in customerTable.Rows){
                
                Customer user = new Customer(row);
                GetAllCustomers.Add(user);
            
                if (CustomerOrderTable != null){
                    user.Cart = CustomerOrderTable.AsEnumerable().Where(r => (int) r["CustomerId"] == user.CustomerId && (int) r["CustomerOrderID"] == 0 ).Select(
                    r => new CustomerOrder(r)
                    ).ToList();
                }
                if (StoreOrderTable != null){
                    user.FinishedOrders = StoreOrderTable.AsEnumerable().Where(r => (int) r["CustomerId"] == user.CustomerId).Select(
                        r => new StoreOrder(r)
                    ).ToList();
                }
                if(CustomerOrderTable != null){
                    foreach(StoreOrder storeOrder in user.FinishedOrders!){
                        storeOrder.Orders = CustomerOrderTable!.AsEnumerable().Where(r => (int) r["CustomerOrderID"] == storeOrder.orderID).Select(
                            r => new CustomerOrder(r)
                        ).ToList();
                        }
                    }
            }
    }
    return GetAllCustomers;
}
/// <summary>
/// Add user takes in info from MainMenu UI and adds it to the database
/// </summary>
/// <param name="userToAdd"></param>
public void AddUser(Customer userToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        
        string sqlCmd = "INSERT INTO Customer (CustomerId, Username, Password) VALUES (@ID, @username, @pass)"; 
        using SqlCommand cmdAddUser= new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddUser.Parameters.AddWithValue("@ID", userToAdd.CustomerId);
        cmdAddUser.Parameters.AddWithValue("@username", userToAdd.UserName);
        cmdAddUser.Parameters.AddWithValue("@pass", userToAdd.PassWord);
        //Executing command
        cmdAddUser.ExecuteNonQuery();
        connection.Close();
        Log.Information("new user added to database {username}", userToAdd.UserName);
    }

/// <summary>
/// Get active user grabs the user that is signed in currently so we can use it when assigning product orders and order history. 
/// </summary>
/// <param name="CustomerId"></param>
/// <returns></returns>
    public Customer GetActiveUser(int CustomerId){
    List<Customer> allUsers = GetAllUsers();
        foreach(Customer user in allUsers){
            
            if(user.CustomerId == CustomerId){
                return user;
            }
        }
        // if you don't find the customer make a new one
        return new Customer();
        
    }


    public void AddCustomerOrder(int CustomerId, CustomerOrder currentcartorder){
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlCmd = "INSERT INTO CustomerOrder (CustomerID,StoreID,CustomerOrderID,ProdID,ProdName,Total,Quantity, ID) VALUES (@customer,@storeID,@customorderid,@productid,@productname,@totalprice,@quantity, @ID)";
        using SqlCommand cmdcartorder= new SqlCommand(sqlCmd, connection);
        cmdcartorder.Parameters.AddWithValue("@customer", currentcartorder.CustomerID );
        cmdcartorder.Parameters.AddWithValue("@storeID", currentcartorder.storeID );
        cmdcartorder.Parameters.AddWithValue("@customorderid", currentcartorder.CustomerOrderID );
        cmdcartorder.Parameters.AddWithValue("@productid", currentcartorder.ProductID );
        cmdcartorder.Parameters.AddWithValue("@productname", currentcartorder.ProductName );
        cmdcartorder.Parameters.AddWithValue("@totalprice", currentcartorder.TotalPrice);
        cmdcartorder.Parameters.AddWithValue("@quantity", currentcartorder.Quantity);
        cmdcartorder.Parameters.AddWithValue("@ID", currentcartorder.ID);
        
        cmdcartorder.ExecuteNonQuery();
        connection.Close();
        Log.Information("new customer order added to database{customer}{totalprice}",currentcartorder.CustomerID,currentcartorder.TotalPrice);
    }

/// <summary>
/// This is used to take the Customer orderid which is set to 0 by default and update the number to what the store its purchased from so we can refrence it later 
/// </summary>
/// <param name="CustomerID"></param>
/// <param name="CustomerOrderID"></param>
    public void UpdateCustomerOrder(int CustomerID, int CustomerOrderID){
    
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();
    string sqlEditCmd = $"UPDATE CustomerOrder SET CustomerOrderID = @CustomerOrderID WHERE CustomerID = @CustomerID AND CustomerOrderID = @0";
    using SqlCommand cmdEditProd= new SqlCommand(sqlEditCmd, connection);
    cmdEditProd.Parameters.AddWithValue("@CustomerOrderID", CustomerOrderID);
    cmdEditProd.Parameters.AddWithValue("@CustomerID", CustomerID);
    cmdEditProd.Parameters.AddWithValue("@0", 0);
    
    cmdEditProd.ExecuteNonQuery();
    connection.Close();
    Log.Information("Customer order has been updated {CustomerOrderID}{CustomerID}",CustomerOrderID,CustomerID);
    
    }



}

