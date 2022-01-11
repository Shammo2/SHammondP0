using Microsoft.Data.SqlClient;
using System.Data;
namespace Models;
public class StoreOrder{

    public StoreOrder(){}

    public StoreOrder(DataRow r){
        orderID = (int) r["OrderID"];
        CustomerID = (int) r["CustomerID"];
        storeID = (int) r["StoreID"];
        TotalAmount = (decimal)r["TotalAmount"];
        OrderDate =r["OrderDate"].ToString();
    }

    public int? orderID { get; set; }

    public int? CustomerID { get; set; }

    public int? storeID {get; set; }
    public string OrderDate { get; set; }

    public decimal TotalAmount { get; set; }
    
    public List<CustomerOrder> Orders { get; set; }

}