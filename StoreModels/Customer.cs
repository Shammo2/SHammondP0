
namespace Models;

public class Customer
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public List<Order> Orders { get; set; }

    public List<Customer> users { get; set; }


  public  Customer(){}

        // public  Customer(string userUserName, string userPassword, int userId){

        //     UserName= userUserName;
        //     PassWord= userPassword;
        //     Id = userId;
        // }


    public  void signup(){
    
    }


    public void signin(){

    }

}

