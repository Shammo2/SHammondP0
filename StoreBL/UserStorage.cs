using Models;
using StoreDL;
namespace StoreBL;

public class UserStorage{

    private DBUserRepo _dl;
    private string _connectionString;

    public UserStorage()
    {
        _connectionString = File.ReadAllText("connectionString.txt");
        _dl = new DBUserRepo(_connectionString);
    }
    public List<Customer> GetAllUsers() 
    {
        return _dl.GetAllUsers();
    }

    public void AddUser(Customer UserToAdd)
    {
        _dl.AddUser(UserToAdd);
    }

    public void AddCustomerOrder(int CustomerId, CustomerOrder currentcartorder){
        _dl.AddCustomerOrder(CustomerId, currentcartorder);
    }

//     public void GetAllUserOrders(int CustomerId){
//     _dl.GetAllUserOrders(CustomerId);
// }

public void UpdateCustomerOrder(int CustomerID, int orderNum){
    _dl.UpdateCustomerOrder(CustomerID,orderNum );
}
public Customer GetActiveUser(int CustomerId){
    return _dl.GetActiveUser(CustomerId);
    
}


}