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
        Console.WriteLine("===What Would you like to do?===");
        Console.WriteLine("[1] View Cart and checkout");
        Console.WriteLine("[2] purchase from stores");
        Console.WriteLine("[3] view order history");
        Console.WriteLine("[x] Exit");
        string menuinput = Console.ReadLine();
            switch(menuinput){

                case "1":
                Console.WriteLine("===You entered: View Cart and checkout===");
                
                List<Customer> allUsers = _BL.GetAllUsers();
                Customer activeuser = _BL.GetActiveUser(CustomerId);
                List<CustomerOrder>  currentCart = activeuser.Cart;
                int storeID =0;
                if(activeuser.Cart == null || currentCart.Count == 0){ 
                    Console.WriteLine(" You have no items ");
                    activeuser.Cart = new List<CustomerOrder>();
                }
                else{
                    storeID = currentCart[0].storeID;
                }
                decimal CustomerTotal =0;
                
                //foreach(CustomerOrder currCart in currentCart ){
                for(int i = 0; i < currentCart.Count; i++){
                Console.WriteLine($"[{i}] Name: {currentCart[i].ProductName}  \nQuantity: {currentCart[i].Quantity} \nPrice: {currentCart[i].TotalPrice} $ " ); 
                
                CustomerTotal += currentCart[i].TotalPrice;
                
                }

                Console.WriteLine(CustomerTotal);
                Console.WriteLine("Checkout? Y/N");
                string checkoutinput = Console.ReadLine();
                if(checkoutinput == "y"){
                Console.WriteLine("You chose checkout");
                
                Checkout(CustomerId,storeID,CustomerTotal, currentCart);
                }
                else{
                    break;
                }


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

                int ItemID = currentproduct.ItemID;
                _storeBL.UpdateProduct(ItemID, (ProductQuantity -(int)Itemquantity));
                Random rand = new Random();
                int orderid = rand.Next(10000);
                CustomerOrder currentcartorder = new CustomerOrder{
                    CustomerID = CustomerId,
                    storeID = AllStores[StoreIndex].StoreID,
                    CustomerOrderID = 0,
                    ProductID = currentproduct.ItemID,
                    ProductName = currentproduct.ProductName,
                    TotalPrice = ((int)Itemquantity * (decimal)currentproduct.Price),
                    Quantity = (int)Itemquantity
                };
                
                _BL.AddCustomerOrder(CustomerId,currentcartorder);
                break;

                case "3":
                Console.WriteLine("you entered view order history");
                List<Customer> allUsers2 = _BL.GetAllUsers();
                Customer activeuser2 = _BL.GetActiveUser(CustomerId);
                List<StoreOrder> FinishedOrders = activeuser2.FinishedOrders;
                
                if(FinishedOrders == null || FinishedOrders.Count == 0){
                Console.WriteLine("No Orders found!");
                activeuser2.FinishedOrders = new List<StoreOrder>();
                }
                foreach(StoreOrder storeorder in FinishedOrders){
                foreach(CustomerOrder Prodorder in storeorder.Orders!){
                    Console.WriteLine($"| {Prodorder.ProductName} | Qty: {Prodorder.Quantity} || ${Prodorder.TotalPrice}");
                }
                }
                break;
                
                case "x":
                exit = true;
                break;
                default:
                Console.WriteLine("Input not found try again");
                break;

                }
    
        }
    Console.WriteLine("goodbye");
}  

public void Checkout(int CustomerId, int storeID, decimal CustomerTotal, List<CustomerOrder> currentCart){

                string currTime = DateTime.Now.ToString();
                double currTimeSeconds = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                Random rnd = new Random();
                int orderNum = rnd.Next(100000);
                // order num is the order number associated with the order we are creating (new store order)
                foreach(CustomerOrder cOrder in currentCart){
                    
                    _BL.UpdateCustomerOrder(CustomerId, orderNum);
                }
                
                StoreOrder newStoreOrder = new StoreOrder {
                orderID = orderNum,
                OrderDate= currTime,
                CustomerID = CustomerId,
                storeID = storeID,
                TotalAmount = CustomerTotal
                };
                _storeBL.AddStoreOrder(newStoreOrder);


}
}

