namespace Models;

public class Storefront
{
    public string Address { get; set; }
    public string Name { get; set; }
    
    public int StoreID {get; set;} 
    public List<Product> Inventory { get; set; }
    public List<Order> Orders { get; set; }

    

}



