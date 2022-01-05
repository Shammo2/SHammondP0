using Models;
using StoreDL;
namespace StoreBL;

public class StoreStorage{

    private StoreRepo _dl;

    public StoreStorage()
    {
        _dl = new StoreRepo();
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
public int GetStoreIndexByID(int StoreID){
    return _dl.GetStoreIndexByID(StoreID);
    
}

public void UpdateProduct(int StoreIndex,int ProductIndex, int Quantity){

     _dl.UpdateProduct(StoreIndex, ProductIndex, Quantity);
}
}