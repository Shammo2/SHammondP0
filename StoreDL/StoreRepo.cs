using System.Text.Json;
using Models;
namespace StoreDL;


public class StoreRepo{

//private static List<Storefront> _allStores = new List<Storefront>();
    private string filePath = "../StoreDL/StoreFront.json";
        public StoreRepo(){}
        public List<Storefront> GetAllStores(){
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Storefront>>(jsonString);
        }

        public List<Product> GetAllProduct(int StoreIndex){
            
            List<Storefront> allstores = GetAllStores();
    
            Storefront selectedstore = allstores[StoreIndex];

            return selectedstore.Inventory;
            
        
        }
        

    public void AddStore(Storefront StoreToAdd)
        {
            List<Storefront> allstores = GetAllStores();
            allstores.Add(StoreToAdd);
            string jsonString = JsonSerializer.Serialize(allstores);
            File.WriteAllText(filePath, jsonString);
        }

public void AddProduct(int StoreIndex, Product ProductToAdd){
    
    List<Storefront> allstores = GetAllStores();
    
    Storefront selectedstore = allstores[StoreIndex];
    
    if(selectedstore.Inventory == null){
        selectedstore.Inventory = new List<Product>();
    }
    
    selectedstore.Inventory.Add(ProductToAdd);
    
    
    string jsonString = JsonSerializer.Serialize(allstores);
    File.WriteAllText(filePath, jsonString);
    }

    public void UpdateProduct(int StoreIndex,int ItemID){

    List<Storefront> allstores = GetAllStores();
    
    Storefront selectedstore = allstores[StoreIndex];

    

    }
public Storefront GetStoreByIndex(int StoreIndex)
    {
        List<Storefront> allStores = GetAllStores();
        return allStores[StoreIndex];
    }

public int GetStoreIndexByID(int StoreID){
        
        List<Storefront> allstores = GetAllStores();
        
        int i=0;
        foreach(Storefront Store in allstores){
            if(StoreID == Store.StoreID){
                return i;
            }
            else{
                i++;
            }

        }
        return i;
    }   


//idk what this is probably not needed 

// List<Storefront> AllStores = GetAllStores();
    // Storefront currStore = GetStoreByID(StoreID);
    // if(currStore.Products == null){
    //     currStore.Products = new List<Product>();
    //     }
    // currStore.Products.Add(productToAdd);
    // string jsonString = JsonSerializer.Serialize(allStores);
    // File.WriteAllText(filePath, jsonString);

}


