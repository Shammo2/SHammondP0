namespace Models;

public class Inventory 
{
    public int StoreId { get; set; }
    public int Quantity { get; set; }
    public Product Item { get; set; }
}