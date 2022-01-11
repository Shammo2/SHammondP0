using Microsoft.Data.SqlClient;
using System.Data;
namespace Models;

public class Customer
{
  public  Customer(){}
  public Customer(DataRow row){
        CustomerId = (int)row["CustomerId"];
        UserName = row["Username"].ToString();
        PassWord = row["Password"].ToString();
    }
    public int CustomerId { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }
    //public List<Order> Orders { get; set; }
    public List<CustomerOrder> Cart { get; set; }
    public List<StoreOrder> FinishedOrders {get; set;}

    //public List<Customer> users { get; set; }

}

