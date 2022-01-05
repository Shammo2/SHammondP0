namespace Models;

public class Cartorder
{
    public Product SelectedProduct { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}