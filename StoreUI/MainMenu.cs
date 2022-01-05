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
        
        Console.WriteLine("[1] sign in");
        Console.WriteLine("[2] create account");
        Console.WriteLine("[3] manager login ");
        Console.WriteLine("[x] exit");
        string signinput= Console.ReadLine();
                    
                    switch(signinput){
                    case "1":
                        bool exists = false;
                        Console.WriteLine("You entered: Sign in");                        
                        Console.WriteLine("enter username");
                        string checkusername = Console.ReadLine();
                        List<Customer> users = _BL.GetAllUsers();
                        string loginpassword = "";
                        int CustomerId = 0;
                        foreach( Customer customer in users){
                            if(checkusername==customer.UserName){
                                Console.WriteLine(" username found ");
                                loginpassword= customer.PassWord;
                                CustomerId = customer.CustomerId;
                                exists=true;
                            }
                            
                        }
                        if(exists){
                        System.Console.WriteLine( "enter password" );
                        string checkpassword = Console.ReadLine();
                        if(loginpassword == checkpassword){

                        Console.WriteLine(" signed in ");
                        new UserMenu().StartUserMenu(CustomerId);
                        }
                        else{
                            Console.WriteLine(" its wrong ");
                        }
                        }

                    break;
                    
                    case "2":
                        Console.WriteLine("You entered: Create Account");
                        Console.WriteLine("pls select a username");
                        string username = Console.ReadLine();
                        System.Console.WriteLine( "pls select a password"  );
                        string password = Console.ReadLine();
                        Console.WriteLine("username:" +username + " Password:" +  password);
                        Random rnd = new Random(10000);
                        int userid = rnd.Next();
                        
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
                    break;
                    case "x":
                    exit = true;
                    break;
        }
    } 
    }  
}



