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



}

