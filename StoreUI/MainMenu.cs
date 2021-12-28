using Models;
using StoreBL;
namespace UI;
public class MainMenu {

    bool exit = false;
    bool signedin = false;
    public void Start() {

    while(!exit){
        if(signedin == false){

        Console.WriteLine("[1] sign in");
        Console.WriteLine("[2] create account");
        Console.WriteLine("[3] manager login ");
        string signinput= Console.ReadLine();
                    
                    switch(signinput){
                    case "1":
                        Console.WriteLine("You entered: Sign in");
                        
                        Console.WriteLine("enter username");
                        var checkusername = Console.ReadLine();
                        System.Console.WriteLine( "enter password" );
                        var checkpassword = Console.ReadLine();

                        Console.WriteLine(checkusername +" " + checkpassword);
                        new Customer().signin();
                        signedin = true;
                    break;
                    
                    case "2":
                        Console.WriteLine("You entered: Create Account");
                        Console.WriteLine("pls select a username");
                        string username = Console.ReadLine();
                        System.Console.WriteLine( "pls select a password"  );
                        string password = Console.ReadLine();
                        Console.WriteLine(username + " " +  password);
                        Random rnd = new Random(10000);
                        int userid = rnd.Next();
                        
                        Customer newUser = new Customer {
                        UserName = username,
                        PassWord =password,
                        Id = userid
                        };
                        _BL.AddUser(newUser);
                        
                        signedin = true;
                    break;

                    case "3":
                    Console.WriteLine(" you entered: manager login ");
                    new ManagerUI().manager();
                    break;
                    }
        }

        Console.WriteLine("[1] Place Order (checkout");
        Console.WriteLine("[2] View stores and inventory");
        Console.WriteLine("[x] Exit");
        string menuinput = Console.ReadLine();
            switch(menuinput){

                case "1":
                Console.WriteLine("You entered: Place Order");
                
                break;

                case "2":
                Console.WriteLine("You entered: View stores and inventory");
                break;

                case "x":
                exit = true;
                break;

                }
    
        }
        Console.WriteLine("goodbye");
    }
}



