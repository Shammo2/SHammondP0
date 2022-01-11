using Models;
using StoreDL;
namespace StoreBL;

public class StoreStorage{

    private DBStoreRepo _dl;
    private string _connectionString;
    
    
    public StoreStorage()
    {
        _connectionString = File.ReadAllText("connectionString.txt");
        _dl = new DBStoreRepo(_connectionString);
    }
    public List<Storefront> GetAllStores() 
    {
        return _dl.GetAllStores();
    }

    public void AddStore(Storefront storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }
    


    public void AddProduct(int StoreIndex, Product ProductToAdd){

    _dl.AddProduct(StoreIndex,ProductToAdd);        
    }   
    

    public List<Product> GetAllProduct(int StoreIndex){

      return _dl.GetAllProduct(StoreIndex);
      
    }
// public int GetStoreIndexByID(int StoreID){
//     return _dl.GetStoreIndexByID(StoreID);
    
// }


public void AddStoreOrder(StoreOrder newStoreOrder){
    _dl.AddStoreOrder(newStoreOrder);
}
public void UpdateProduct(int ItemID, int Quantity){

    _dl.UpdateProduct(ItemID, Quantity);
}
}