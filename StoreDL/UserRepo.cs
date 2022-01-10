using System.Text.Json;
using Models;
namespace StoreDL;

public class UserRepo{

    private string filePath = "../StoreDL/Users.json";
        public 
        UserRepo(){}
        public List<Customer> GetAllUsers(){
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Customer>>(jsonString);
        }
        

    public void AddUser(Customer UserToAdd)
        {
            List<Customer> allUsers = GetAllUsers();
            allUsers.Add(UserToAdd);
            string jsonString = JsonSerializer.Serialize(allUsers);
            File.WriteAllText(filePath, jsonString);
        }
    public Customer GetActiveUser(int CustomerId){
        List<Customer> allUsers = GetAllUsers();
        Customer currUser = new Customer();
        foreach(Customer user in allUsers){
            if (user.CustomerId == CustomerId){
                currUser = user;
            }
        }
        return currUser;
    }

    public void AdditemToCart(int CustomerId, CustomerOrder currentcartorder){
        List<Customer> allUsers = GetAllUsers();
        Customer activeuser = GetActiveUser(CustomerId);
        
        if(activeuser.Cart == null){
            activeuser.Cart = new List<CustomerOrder>();
        }
        int i = 0;
        foreach(Customer user in allUsers){
            if(CustomerId ==user.CustomerId){
            break;
            }
            else{
                i++;
            }
        } 
        activeuser.Cart.Add(currentcartorder);
        allUsers[i] = activeuser;
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath, jsonString);
    }

// public void GetAllUserOrders(int CustomerId){

  //List<Customer> allUsers = GetAllUsers();
  //Customer activeuser = GetActiveUser(CustomerId); 

// // int i = 0;
// //     foreach(Customer user in allUsers){
// //         if(CustomerId ==user.CustomerId){
// //            break;
// //         }
// //         else{
// //             i++;
// //         }
// // }
// // allUsers[i] = activeuser;

// }



}//class end
