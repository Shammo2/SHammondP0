namespace Models;

public class Cart
{
    public Product ItemID { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}