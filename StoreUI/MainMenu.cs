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
    bool signedin = false;
    public void Start() {

while(!signedin){   
        
        Console.WriteLine("[1] sign in");
        Console.WriteLine("[2] create account");
        Console.WriteLine("[3] manager login ");
        string signinput= Console.ReadLine();
                    
                    switch(signinput){
                    case "1":
                        bool exists = false;
                        Console.WriteLine("You entered: Sign in");                        
                        Console.WriteLine("enter username");
                        string checkusername = Console.ReadLine();
                        List<Customer> users = _BL.GetAllUsers();
                        string loginpassword = "";
                        foreach( Customer customer in users){
                            if(checkusername==customer.UserName){
                                Console.WriteLine(" username found ");
                                loginpassword= customer.PassWord;
                                exists=true;
                            }
                            
                        }
                        if(exists){
                        System.Console.WriteLine( "enter password" );
                        string checkpassword = Console.ReadLine();
                        if(loginpassword == checkpassword){

                        Console.WriteLine(" signed in ");
                        signedin = true;
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
                        
                        signedin = true;
                    break;

                    case "3":
                    Console.WriteLine(" you entered: manager login ");
                    new ManagerUI().manager();
                    break;
                    
        }
}   
while(!exit){        
        Console.WriteLine("====Welcome To Fruits-R-Us====");
        Console.WriteLine("[1] checkout");
        Console.WriteLine("[2] View stores and inventory");
        Console.WriteLine("[x] Exit");
        string menuinput = Console.ReadLine();
            switch(menuinput){

                case "1":
                Console.WriteLine("===You entered: Checkout===");
                
                break;

                case "2":
                Console.WriteLine("You entered: View stores and inventory");
                Console.WriteLine(" Stores: ");
                List<Storefront> AllStores = _storeBL.GetAllStores();
                Console.WriteLine(" ===Stores=== ");
                
                if(AllStores.Count == 0){
                Console.WriteLine(" No stores found ");
                }
                for(int i = 0; i < AllStores.Count; i++){
                Console.WriteLine($"[{i}] Name: {AllStores[i].Name} \nCity: {AllStores[i].Address}") ;
                }
                int? selection = Int32.Parse(Console.ReadLine());
                
                int StoreIndex= (int)selection;
                
                List<Product> StoreInventory =_storeBL.GetAllProduct(StoreIndex);
                for(int i = 0; i < StoreInventory.Count; i++){
                   Console.WriteLine($"[{i}] Name: {StoreInventory[i].ProductName} \nDescription: {StoreInventory[i].Description} \nQuantity: {StoreInventory[i].Quantity} \nPrice: {StoreInventory[i].Price}" ); 
                }
                
                Console.WriteLine("Select Products to add to cart  ");
                int? Itemselection = Int32.Parse(Console.ReadLine());
                
                int productIndex= (int)Itemselection;
                break;
                
                case "x":
                exit = true;
                break;

                }
    
        }
        Console.WriteLine("goodbye");
    
}
}


