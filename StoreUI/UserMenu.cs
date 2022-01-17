using Models;
using System.Text.RegularExpressions;
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
                Console.WriteLine("---Total Price--- ");
                Console.WriteLine(CustomerTotal);
                Console.WriteLine("Checkout? Y/N");
                string checkoutinput = Console.ReadLine();
                if(checkoutinput == "y"){
                Console.WriteLine("Checkout Complete");
                
                Checkout(CustomerId,storeID,CustomerTotal, currentCart);
                }
                else{
                    break;
                }


                break;

                case "2":
                Console.WriteLine("===You entered: View stores and inventory===");
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

                int StoreID = AllStores[StoreIndex].StoreID;
                
                List<Product> StoreInventory =_storeBL.GetAllProduct(StoreID);
                for(int i = 0; i < StoreInventory.Count; i++){
                Console.WriteLine($"[{i}] Name: {StoreInventory[i].ProductName} \nDescription: {StoreInventory[i].Description} \nQuantity: {StoreInventory[i].Quantity} \nPrice: {StoreInventory[i].Price}" ); 
                }
                
                //product select
                Console.WriteLine("Select Products to add to cart ");
                int Itemselection = Int32.Parse(Console.ReadLine());
                int productIndex= Itemselection;
                
                
                // quantity
                Product currentproduct = StoreInventory[productIndex];
                Console.WriteLine("Select Quantity ");
                int? Itemquantity = Int32.Parse(Console.ReadLine());
                
                int ProductQuantity = currentproduct.Quantity;
                int ItemID = currentproduct.ItemID;
                _storeBL.UpdateProduct(ItemID, (ProductQuantity -(int)Itemquantity));
                Random rand = new Random();
                int orderid = rand.Next(10000);
                
                try{
                CustomerOrder currentcartorder = new CustomerOrder{
                    CustomerID = CustomerId,
                    storeID = AllStores[StoreIndex].StoreID,
                    CustomerOrderID = 0,
                    ID =orderid,
                    ProductID = currentproduct.ItemID,
                    ProductName = currentproduct.ProductName,
                    TotalPrice = ((int)Itemquantity * (decimal)currentproduct.Price),
                    Quantity = (int)Itemquantity
                };
                _BL.AddCustomerOrder(CustomerId,currentcartorder);
                }

                catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
                break;

                case "3":
                Console.WriteLine("===you entered view order history===");
                List<Customer> allUsers2 = _BL.GetAllUsers();
                Customer activeuser2 = _BL.GetActiveUser(CustomerId);
                List<StoreOrder> FinishedOrders = activeuser2.FinishedOrders;
                while(!exit){
                    if(FinishedOrders == null || FinishedOrders.Count == 0){
                    Console.WriteLine("No Orders found!");
                    activeuser2.FinishedOrders = new List<StoreOrder>();
                    }
                    foreach(StoreOrder storeorder in FinishedOrders){
                        Console.WriteLine(storeorder.OrderDate);
                        foreach(CustomerOrder Prodorder in storeorder.Orders!){
                            Console.WriteLine($"| {Prodorder.ProductName} | Qty: {Prodorder.Quantity} || ${Prodorder.TotalPrice}");
                            
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
                        FinishedOrders.Sort((x, y) => x.TotalAmount.CompareTo(y.TotalAmount));
                    }
                    if(sortselection =="2"){
                    FinishedOrders.Sort((x, y) => y.OrderDate.CompareTo(x.OrderDate));
                    }
                    if(sortselection =="3"){
                        FinishedOrders.Sort((x,y) => y.TotalAmount.CompareTo(x.TotalAmount));
                    }
                    if(sortselection =="4"){
                    FinishedOrders.Sort((x,y) => x.OrderDate.CompareTo(y.OrderDate));
                    }
                    if(sortselection =="x"){
                        exit = true;
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

