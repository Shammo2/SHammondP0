namespace Models;

public class Customer
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public List<Order> Orders { get; set; }

    public List<Customer> users { get; set; }




  public  Customer(){
            
        }

        public  Customer(string userUserName, string userPassword, int userId){

            UserName= userUserName;
            PassWord= userPassword;
            Id = userId;
        }




    public void SigninCheck(){



    }

    public  void signup(){
        Console.WriteLine("pls select a username");
        UserName = Console.ReadLine();
        System.Console.WriteLine( "pls select a password"  );
        PassWord = Console.ReadLine();
        Console.WriteLine(UserName + " " +  PassWord);
        //Customer.Add(user);
    }


    public void signin(){

        Console.WriteLine("enter username");
        var checkusername = Console.ReadLine();
        System.Console.WriteLine( "enter password" );
        var checkpassword = Console.ReadLine();

        Console.WriteLine(checkusername +" " + checkpassword);

        

    }

}

