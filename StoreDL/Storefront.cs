using System.Text.Json;
using Models;
namespace StoreDL;


public class StoreRepo{

    private string filePath = "../StoreDL/StoreFront.json";
        public StoreRepo(){}
        public List<Storefront> GetAllStores(){
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Storefront>>(jsonString);
        }
        

    public void AddStore(Storefront storeToAdd)
        {
            List<Storefront> allstores = GetAllStores();
            allstores.Add(storeToAdd);
            string jsonString = JsonSerializer.Serialize(allstores);
            File.WriteAllText(filePath, jsonString);
        }
}


