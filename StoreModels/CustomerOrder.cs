using Microsoft.Data.SqlClient;
using System.Data;
namespace Models;
public class CustomerOrder{
    
public CustomerOrder(){}
public CustomerOrder(DataRow r){

        CustomerID = (int) r["CustomerID"];
        storeID = (int) r["StoreID"];
        CustomerOrderID = (int) r["CustomerOrderID"];
        ProductID = (int)r["prodID"];
        ProductName = r["ProdName"].ToString() ?? "";
        TotalPrice = (decimal)r["Total"];
        Quantity = (int) r["Quantity"];
        ID =(int)r["ID"];
    }


    public int CustomerID { get; set; }
    public int storeID { get; set; }
    public int CustomerOrderID { get; set; }
    public int ProductID { get; set; }
    public string ProductName{ get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity {get; set;}
    public int ID {get; set;}
}