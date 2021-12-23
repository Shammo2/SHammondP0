namespace Models;

public class LineItem
{
    public Product Item { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}