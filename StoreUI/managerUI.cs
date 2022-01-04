
using Models;
using StoreBL;
namespace UI;



    
public class ManagerUI{
         
         private StoreStorage _BL;
         public ManagerUI() 
        {
        _BL = new StoreStorage();
        }
        
        public void StartManager(){
           
       
        bool managerExit = false;
        while(managerExit == false){
        Console.WriteLine("[1] Manage inventory");
        Console.WriteLine("[2] view order history");
        Console.WriteLine("[3] make a new store  ");
        Console.WriteLine("[x] exit  ");
        List<Storefront> AllStores = _BL.GetAllStores();
        string Minput = Console.ReadLine();
        switch(Minput){
            
            
            case "1":
                    Console.WriteLine("You Entered: pick a store");
                    Console.WriteLine("[1] Update Product ");
                    Console.WriteLine("[2] Add product ");
                    string manageinput = Console.ReadLine();
                    switch(manageinput){

                    case "1":    
                    Console.WriteLine("You Chose: Update Product  ");
                    for(int i = 0; i < AllStores.Count; i++)
                    {
                        Console.WriteLine($"[{i}] Name: {AllStores[i].Name} \nAddress: {AllStores[i].Address}");
                    }
                    Console.WriteLine("Select a store");
                    int storeselection = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(storeselection);
                    int StoreIndex1= (int)storeselection;
                    Console.WriteLine("Select a Item To update");
                    List<Product> StoreInventory =_BL.GetAllProduct(StoreIndex1);
                    for(int i = 0; i < StoreInventory.Count; i++){
                        Console.WriteLine($"[{i}] Name: {StoreInventory[i].ProductName} \nDescription: {StoreInventory[i].Description} \nQuantity: {StoreInventory[i].Quantity} \nPrice: {StoreInventory[i].Price}" ); 
                    }

                    
                    break;

                    case "2":
                    Console.WriteLine("You Chose: Add product ");
                    if(AllStores.Count == 0){
                        Console.WriteLine(" No stores found ");
                    }
                    else{
                        Console.WriteLine(" ==Stores== ");
                        for(int i = 0; i < AllStores.Count; i++){
                        Console.WriteLine($"[{i}] Name: {AllStores[i].Name} \nAddress: {AllStores[i].Address}");
                        }
                        int selection = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(selection);
                        Console.WriteLine(" you picked " + selection );
                        Console.WriteLine("Enter item: name");
                        string prodname = Console.ReadLine();
                        Console.WriteLine("Enter item: description");
                        string description = Console.ReadLine();
                        Console.WriteLine("Enter item: quantity");
                        int quantity = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter item: Price");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter item: ID");
                        int ItemId = Int32.Parse(Console.ReadLine());
                        int StoreIndex= selection;
                        
                        Product ProductToAdd = new Product{
                            ProductName = prodname,
                            Description =description,
                            Price = price,
                            Quantity = quantity,
                            ItemID = ItemId
                        };
                        _BL.AddProduct(StoreIndex,ProductToAdd);
                        Console.WriteLine(" Product added "); 
                    }
                    break;  
                    }               

            break;
            case "2":
                    Console.WriteLine("You Entered: view order history");
                    List<Storefront> storeorders = _BL.GetAllStores();
                    for(int i = 0; i < storeorders.Count; i++)
                    {
                        Console.WriteLine($"[{i}] Name: {storeorders[i].Name} \nAddress: {storeorders[i].Address}");
                    }
                    int orderselection = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(orderselection);
            break;
            case"3":
                    Console.WriteLine("You Entered: make a store ");
                    Console.WriteLine("Create a new Store:");
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Address: ");
                    string address = Console.ReadLine();
                    Random rnd = new Random(10000);
                    int storeid = rnd.Next();
                    
                    Storefront newStore = new Storefront {
                        Name = name,
                        Address =address,
                        StoreID = storeid
                    };
                    _BL.AddStore(newStore);
            break;
            case "x":
            managerExit = true;
            break;
    }
    new MainMenu().Start();
}
}
public void manager(){
        //this checks the manager password 
        bool Mexit= false;
        
        while(Mexit == false){
        string mPass = "1234";
        string Muser = "manager";


        Console.WriteLine("enter username");
        var managerUser = Console.ReadLine();
        System.Console.WriteLine("enter password");
        var managerPass = Console.ReadLine();
        
        if(mPass==managerPass && Muser == managerUser){

            Console.WriteLine("you got it");
            StartManager();
            Mexit = true;
        }
        else{
            Console.WriteLine("try again");
        }
     }
    
        
    } 

    public void ManageInventory(){




    }

    public void OrderHistory(){

        


    }
}

