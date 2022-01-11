using Microsoft.Data.SqlClient;
using System.Data;
namespace Models;
public class Product
{

public Product(){}
public Product(DataRow row){
        ProductName = row["ProdName"].ToString();
        Description = row["ProdDescription"].ToString();
        Price = (decimal)row["ProdPrice"];
        Quantity = (int)(row)["ProdQuantity"];
        ItemID = (int)row["Id"];
        StoreID = (int)row["StoreID"];
    }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity {get; set;}
        public int ItemID {get; set;}
        public int StoreID {get; set;}

}


