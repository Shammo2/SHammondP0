using Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Serilog;
namespace StoreDL;

public class DBStoreRepo{
    
    private string _connectionString;
    public DBStoreRepo(string connectionString){
    _connectionString = connectionString;
    _connectionString = File.ReadAllText("connectionString.txt");
    
}
/// <summary>
/// Get all stores is used to pull all the stores and any related data from the SQL table
/// </summary>
/// <returns></returns>
public List<Storefront> GetAllStores(){
    List<Storefront> allstores = new List<Storefront>();
    using SqlConnection connection = new SqlConnection(_connectionString);

    string storeSelection = "SELECT * FROM Storefront";
    string productSelect = "Select * From Product";
    string storeOrderSelect = "Select * From StoreOrder";
    string CustomerOrderSelect = "Select * From CustomerOrder";
    
    DataSet StoreSet = new DataSet();
    //adapters for the tables
    using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeSelection, connection);
    using SqlDataAdapter prodAdapter = new SqlDataAdapter(productSelect, connection);
    using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);
    using SqlDataAdapter CustomerOrderAdapter = new SqlDataAdapter(CustomerOrderSelect, connection);
    //filling each table using the adapters
    storeAdapter.Fill(StoreSet, "Storefront");
    prodAdapter.Fill(StoreSet,"Product");
    storeOrderAdapter.Fill(StoreSet,"StoreOrder");
    CustomerOrderAdapter.Fill(StoreSet,"CustomerOrder");

    DataTable storeTable = StoreSet.Tables["Storefront"];
    DataTable productTable = StoreSet.Tables["Product"];
    DataTable storeOrderTable = StoreSet.Tables["StoreOrder"];
    DataTable CustomerOrderTable = StoreSet.Tables["CustomerOrder"];
        if(storeTable != null){   
            foreach(DataRow row in storeTable.Rows){
                    
                    Storefront StoreFront = new Storefront(row);
                    
                
                if (productTable != null){
                        StoreFront.Inventory = productTable.AsEnumerable().Where(r => (int) r["Id"] == StoreFront.StoreID).Select(
                            r => new Product(r)
                        ).ToList();
                    }
                if (storeOrderTable != null){
                        StoreFront.StoreOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["StoreID"] == StoreFront.StoreID).Select(
                            r => new StoreOrder(r)
                        ).ToList();
                    }
                if(CustomerOrderTable != null){
                        foreach(StoreOrder StoreOrder in StoreFront.StoreOrders){
                            StoreOrder.Orders = CustomerOrderTable.AsEnumerable().Where(r => (int) r["CustomerOrderID"] == StoreOrder.orderID).Select(
                                r => new CustomerOrder(r)
                            ).ToList();
                            }
                }
                allstores.Add(StoreFront);
            }
        
        }
        return allstores;
}
/// <summary>
/// Get all products is used to grab each product from the selected store (by index)
/// </summary>
/// <param name="StoreIndex"></param>
/// <returns></returns>
public List<Product> GetAllProduct(int StoreID){
    List<Product> allProd = new List<Product>();
    using SqlConnection connection = new SqlConnection(_connectionString);
        string ProdSelect = $"Select * From Product WHERE StoreID = {StoreID}";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter ProdAdapter = new SqlDataAdapter(ProdSelect, connection);
        ProdAdapter.Fill(ProdSet, "Product");
        DataTable ProdTable = ProdSet.Tables["Product"];
        if(ProdTable!= null){   
            foreach(DataRow row in ProdTable.Rows){
                
                Product prod = new Product(row);
                allProd.Add(prod);
            }
        }
        return  allProd;
}

/// <summary>
/// This is used to take in a product and update the quantity from the manager UI
/// </summary>
/// <param name="ItemID"></param>
/// <param name="Quantityadjust"></param>
public void UpdateProduct(int ItemID, int quantityadjust){
    List<Storefront> allstores = GetAllStores();
    using SqlConnection connection = new SqlConnection(_connectionString);
    connection.Open();
    string sqlEditCmd = $"UPDATE Product SET ProdQuantity = @qty WHERE ID = @prodID";
    using SqlCommand cmdEditProd= new SqlCommand(sqlEditCmd, connection);
    cmdEditProd.Parameters.AddWithValue("@qty", quantityadjust);
    cmdEditProd.Parameters.AddWithValue("@prodID", ItemID);
    cmdEditProd.ExecuteNonQuery();
    connection.Close();

}
public List<StoreOrder> GetAllOrders(int StoreIndex){
    List<StoreOrder> allStoreOrders = new List<StoreOrder>();
    using SqlConnection connection = new SqlConnection(_connectionString);
        
        string ProdSelect = "Select * From StoreOrder";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter StoreOrderAdapter = new SqlDataAdapter(ProdSelect, connection);
        
        StoreOrderAdapter.Fill(ProdSet, "StoreOrder");
        
        DataTable StoreOrder = ProdSet.Tables["StoreOrder"];
        if(StoreOrder!= null){   
            foreach(DataRow row in StoreOrder.Rows){
                StoreOrder Orders = new StoreOrder(row);
                allStoreOrders.Add(Orders);
            }
        }
        
    return  allStoreOrders;
}


/// <summary>
/// This is Used to add a new store through the manager UI
/// </summary>
/// <param name="StoreToAdd"></param>
public void AddStore(Storefront StoreToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        
        string sqlCmd = "INSERT INTO Storefront (ID, Address, Name ) VALUES (@ID, @address, @name)"; 
        using SqlCommand cmdAddStore= new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddStore.Parameters.AddWithValue("@ID", StoreToAdd.StoreID);
        cmdAddStore.Parameters.AddWithValue("@address", StoreToAdd.Address);
        cmdAddStore.Parameters.AddWithValue("@name", StoreToAdd.Name);
        
        cmdAddStore.ExecuteNonQuery();
        connection.Close();
    }

    public void AddProduct(int StoreIndex, Product ProductToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        
        string sqlCmd = "INSERT INTO Product (Id,ProdName,ProdDescription,ProdPrice,ProdQuantity, StoreID) VALUES (@Id, @name, @description, @price, @quantity, @storeID)"; 
        using SqlCommand cmdAddProd= new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddProd.Parameters.AddWithValue("@Id", ProductToAdd.ItemID);
        cmdAddProd.Parameters.AddWithValue("@name", ProductToAdd.ProductName);
        cmdAddProd.Parameters.AddWithValue("@description", ProductToAdd.Description);
        cmdAddProd.Parameters.AddWithValue("@price", ProductToAdd.Price);
        cmdAddProd.Parameters.AddWithValue("@quantity", ProductToAdd.Quantity);
        cmdAddProd.Parameters.AddWithValue("@storeID", ProductToAdd.StoreID);
        
        cmdAddProd.ExecuteNonQuery();
        connection.Close();
        Log.Information("Product added {name}{price}{quantity}", ProductToAdd.ProductName,ProductToAdd.Price,ProductToAdd.Quantity);
    }

    public void AddStoreOrder(StoreOrder newStoreOrder){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlInsertCmd = "INSERT INTO StoreOrder (OrderID, CustomerID, storeID, TotalAmount, OrderDate) VALUES (@orderId, @CustomerId, @storeId, @total, @OrderDate)";
        //Creates the new sql command
        using SqlCommand cmd = new SqlCommand(sqlInsertCmd, connection);

        cmd.Parameters.AddWithValue("@orderId", newStoreOrder.orderID);
        cmd.Parameters.AddWithValue("@CustomerId", newStoreOrder.CustomerID);
        cmd.Parameters.AddWithValue("@storeId", newStoreOrder.storeID);
        cmd.Parameters.AddWithValue("@total", newStoreOrder.TotalAmount);
        cmd.Parameters.AddWithValue("@OrderDate", newStoreOrder.OrderDate);

        cmd.ExecuteNonQuery();
        connection.Close();
        Log.Information("Store Order Added {orderId}{storeId}{total}", newStoreOrder.orderID,newStoreOrder.storeID,newStoreOrder.TotalAmount);

    }
    
    


}


