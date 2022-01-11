
using Models;
using StoreBL;

namespace UI;


public class ManagerUI{
         
         private StoreStorage _BL;
         public ManagerUI(){
        _BL = new StoreStorage();
        }
        
        public void StartManager(){

        bool managerExit = false;
        while(managerExit == false){
        Console.WriteLine(" ===Welcome to Manager menu=== ");
        Console.WriteLine("[1] Manage inventory");
        Console.WriteLine("[2] view order history");
        Console.WriteLine("[3] make a new store  ");
        Console.WriteLine("[x] exit  ");
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
                    UpdateProduct();
                    break;

                    case "2":
                    Console.WriteLine("You Chose: Add product ");
                    AddProduct();
                    break;  
                    }               

            break;
            case "2":
                Console.WriteLine("You Entered: view order history");
                ViewOrderHistory();
            break;
            
            case"3":
            MakeAStore();
            
            break;
            
            case "x":
            managerExit = true;
            break;
            
            default:
            Console.WriteLine("Input not found try again");
            break;
    }
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

private void ViewOrderHistory(){
    List<Storefront> AllStores = _BL.GetAllStores();
    
    for(int i = 0; i < AllStores.Count; i++){
        Console.WriteLine($"[{i}] Name: {AllStores[i].Name} \nAddress: {AllStores[i].Address}");
    }
    Console.WriteLine("Select a store");
    int storeselection = Int32.Parse(Console.ReadLine());
    Console.WriteLine(storeselection);
    int StoreIndex= (int)storeselection;
    List<StoreOrder> StoreOrders = AllStores[StoreIndex].StoreOrders;
    bool exit = false;
    
    while(!exit){
        foreach(StoreOrder storeorder in StoreOrders){
            Console.WriteLine($"\nPlaced on {storeorder.OrderDate} by {storeorder.CustomerID}");

            foreach(CustomerOrder Order in storeorder.Orders){
            Console.WriteLine($"| {Order.ProductName} | Qty: {Order.Quantity} || ${Order.TotalPrice}");
            }
            Console.WriteLine("Total $:" + storeorder.TotalAmount);
            Console.WriteLine("-------------------");
            }
        
        Console.WriteLine("[1] Sort by Lowest Price");
        Console.WriteLine("[2] Sort by Newest Date");
        Console.WriteLine("[3] Sort by Highest Price");
        Console.WriteLine("[4] Sort by Oldest Date");
        Console.WriteLine("[x] exit");
        string sortselection = Console.ReadLine();
        if(sortselection =="1"){
            StoreOrders.Sort((x, y) => x.TotalAmount.CompareTo(y.TotalAmount));
        }
        if(sortselection =="2"){
        StoreOrders.Sort((x, y) => y.OrderDate.CompareTo(x.OrderDate));
        }
        if(sortselection =="3"){
            StoreOrders.Sort((x,y) => y.TotalAmount.CompareTo(x.TotalAmount));
        }
        if(sortselection =="4"){
        StoreOrders.Sort((x,y) => x.OrderDate.CompareTo(y.OrderDate));
        }
        if(sortselection =="x"){
            exit = true;
        }
    }
}
    private void MakeAStore(){
    List<Storefront> AllStores = _BL.GetAllStores();
    Console.WriteLine("You Entered: make a store ");
            Console.WriteLine("Create a new Store:");
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Address: ");
            string address = Console.ReadLine();
            Random rnd = new Random();
            int storeid = rnd.Next(10000);
            
            Storefront newStore = new Storefront {
                Name = name,
                Address =address,
                StoreID = storeid
            };
            _BL.AddStore(newStore);

    }

    private void UpdateProduct(){
    // This gets the product by index and updates it
    List<Storefront> AllStores = _BL.GetAllStores();
    
            for(int i = 0; i < AllStores.Count; i++)
            {
                Console.WriteLine($"[{i}] Name: {AllStores[i].Name} \nAddress: {AllStores[i].Address}");
            }
            Console.WriteLine("Select a store");
            int storeselection = Int32.Parse(Console.ReadLine());
            Console.WriteLine(storeselection);
            int StoreIndex1= (int)storeselection;
            Console.WriteLine("Select a Item To update");
            
            int StoreID= AllStores[StoreIndex1].StoreID;
            List<Product> StoreInventory =_BL.GetAllProduct(StoreID);
            for(int i = 0; i < StoreInventory.Count; i++){
                Console.WriteLine($"[{i}] Name: {StoreInventory[i].ProductName} \nDescription: {StoreInventory[i].Description} \nQuantity: {StoreInventory[i].Quantity} \nPrice: {StoreInventory[i].Price}" ); 
            }
            Console.WriteLine("Select a product");
            int productselection = Int32.Parse(Console.ReadLine());
            Console.WriteLine("you chose" + StoreInventory[productselection].ProductName);
            Console.WriteLine("Enter quantity");
            int quantityadjust = Int32.Parse(Console.ReadLine());

            int ProductID =StoreInventory[productselection].ItemID;
            
            _BL.UpdateProduct(ProductID,quantityadjust);

    }

    private void AddProduct(){
        // This adds a new product to the list
       List<Storefront> AllStores = _BL.GetAllStores();
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
                    int StoreIndex= selection;
                    Random rnd = new Random();
                    int ItemID = rnd.Next(10000);
                    
                    Product ProductToAdd = new Product{
                        StoreID =  AllStores[selection].StoreID,
                        ProductName = prodname,
                        Description =description,
                        Price = price,
                        Quantity = quantity,
                        ItemID = ItemID
                };
                _BL.AddProduct(StoreIndex,ProductToAdd);
                Console.WriteLine(" Product added "); 
            }

    }
}

