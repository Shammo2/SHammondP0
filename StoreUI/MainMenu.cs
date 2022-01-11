using Microsoft.Data.SqlClient;
using System.Data;
using Serilog;
using Models;
using StoreBL;
namespace UI;
public class MainMenu {
    private StoreStorage _storeBL;  
    private UserStorage _BL;
        public MainMenu() 
        {
        _BL = new UserStorage();
        _storeBL = new StoreStorage();
        }
        
    bool exit = false;
    
    public void Start() {

while(!exit){   
        Console.WriteLine("===Welcome To Fruits-R-Us===");
        Console.WriteLine("[1] sign in");
        Console.WriteLine("[2] create account");
        Console.WriteLine("[3] manager login ");
        Console.WriteLine("[x] exit");
        string signinput= Console.ReadLine();
                    
                    switch(signinput){
                    case "1":
                        Console.WriteLine("You entered: Sign in");                        
                        Console.WriteLine("enter username");
                        string checkusername = Console.ReadLine();
                        List<Customer> users = _BL.GetAllUsers();
                        bool exists = false;
                        string loginpassword = "";
                        int CustomerId = 0;
                        foreach( Customer customer in users){
                            if(checkusername==customer.UserName){
                                Console.WriteLine("username found");
                                loginpassword= customer.PassWord;
                                CustomerId = customer.CustomerId;
                                exists=true;
                            }
                            
                        }
                        if(exists){
                            System.Console.WriteLine("enter password");
                            string checkpassword = Console.ReadLine();
                            
                            if(loginpassword == checkpassword){

                            Console.WriteLine("signed in ");
                            new UserMenu().StartUserMenu(CustomerId);
                            Log.Information("user signed in {CustomerId}",CustomerId );
                            }
                            else{
                                Console.WriteLine("Try again");
                            }

                        }
                        else{
                            Console.WriteLine("username not found");
                        }

                    break;
                    
                    case "2":
                        Console.WriteLine("You entered: Create Account");
                        Console.WriteLine("pls select a username");
                        string username = Console.ReadLine();
                        System.Console.WriteLine( "pls select a password"  );
                        string password = Console.ReadLine();
                        Console.WriteLine("username:" +username + " Password:" +  password);
                        Random rnd = new Random();
                        int userid = rnd.Next(10000);
                        
                        Customer newUser = new Customer {
                        UserName = username,
                        PassWord =password,
                        CustomerId = userid
                        };
                        _BL.AddUser(newUser);
                        new UserMenu().StartUserMenu(userid);
                    break;

                    case "3":
                    Console.WriteLine(" you entered: manager login ");
                    new ManagerUI().manager();
                    Log.Information("Manager login {now}",DateTime.Now);
                    break;
                    case "x":
                    exit = true;
                    break;
                    default:
                    Console.WriteLine("Input not found try again");
                    break;
        }
    } 
    }  
}



