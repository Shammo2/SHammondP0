using Models;
using StoreDL;
namespace StoreBL;

public class UserStorage{

    private UserRepo _dl;

    public UserStorage()
    {
        _dl = new UserRepo();
    }
    public List<Customer> GetAllUsers() 
    {
        return _dl.GetAllUsers();
    }

    public void AddUser(Customer UserToAdd)
    {
        _dl.AddUser(UserToAdd);
    }

    
        public void AdditemToCart(int CustomerId, Cartorder currentcartorder){
           _dl.AdditemToCart(CustomerId,currentcartorder);
        }
//     public void GetAllUserOrders(int CustomerId){
//     _dl.GetAllUserOrders(CustomerId);
// }

public Customer GetActiveUser(int CustomerId){
    return _dl.GetActiveUser(CustomerId);
    
}


}