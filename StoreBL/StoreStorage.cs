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


}