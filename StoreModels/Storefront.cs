using Microsoft.Data.SqlClient;
using System.Data;
namespace Models;

public class Storefront
{
    public Storefront(){}
    public Storefront(DataRow row){
        Address = row["Address"].ToString();
        Name = row["Name"].ToString();
        StoreID = (int)row["Id"];
    }
    public string Address { get; set; }
    public string Name { get; set; }
    public int StoreID {get; set;} 
    public List<Product> Inventory { get; set; }
    public List<StoreOrder> StoreOrders { get; set; }

    

}



