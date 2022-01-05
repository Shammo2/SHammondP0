using Models;
using StoreBL;
namespace UI;


public class UserMenu{
    private StoreStorage _storeBL;  
private UserStorage _BL;
    
    public UserMenu(){
        _BL = new UserStorage();
        _storeBL = new StoreStorage();
    }
public void StartUserMenu(int CustomerId){
bool exit = false;
while(!exit){        
        Console.WriteLine("====Welcome To Fruits-R-Us====");
        Console.WriteLine("[1] checkout");
        Console.WriteLine("[2] purchase from stores");
        Console.WriteLine("[3] view order history");
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
                
                Product currentproduct = StoreInventory[productIndex];
                Console.WriteLine("Select Quantity ");
                int? Itemquantity = Int32.Parse(Console.ReadLine());

                int ProductQuantity = currentproduct.Quantity;

                _storeBL.UpdateProduct(StoreIndex, productIndex, (ProductQuantity -(int)Itemquantity));
                Random rnd = new Random(10000);
                int orderid = rnd.Next();
                Cartorder currentcartorder = new Cartorder{
                    SelectedProduct = currentproduct,
                    OrderId = orderid,
                    Quantity = (int)Itemquantity,

                };

                _BL.AdditemToCart(CustomerId,currentcartorder);
                break;

                case "3":
                Console.WriteLine("you entered view order history");
                
                // List<Cartorder> Orderhistory =_BL.GetAllUserOrders(CustomerId);
                    
                //     for(int i = 0; i < Orderhistory.Count; i++){
                //     Console.WriteLine($"[{i}] Name: {Orderhistory[i].SelectedProduct} \nDescription: {Orderhistory[i].Quantity} \nQuantity: {Orderhistory[i].OrderId}" ); 
                //     }

                break;
                
                case "x":
                exit = true;
                break;

                }
    
        }
        Console.WriteLine("goodbye");
}  
}

