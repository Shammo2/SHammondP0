using Models;
using StoreBL;
namespace UI;



    
public class ManagerUI{
         
         private StoreStorage _BL;
         public ManagerUI() 
        {
        _BL = new StoreStorage();
        }
        
        public void StartManager(){
           
       
        bool managerExit = false;
        while(managerExit == false) {
        Console.WriteLine("[1] Manage inventory");
        Console.WriteLine("[2] view order history");
        Console.WriteLine("[x] exit  ");

        string Minput = Console.ReadLine();
        switch(Minput){

            case "1":
            Console.WriteLine("[1] pick a store");
            Console.WriteLine("[2] make a store  ");
            Console.WriteLine("[x] exit  ");
            string storechoice = Console.ReadLine();
            switch(storechoice)
            {

               case "1":
               Console.WriteLine(" pick a store");
               List<Storefront> AllStores = _BL.GetAllStores();
               
               if(AllStores.Count == 0){
                   Console.WriteLine(" No stores found ");
                   }
                else{
                    Console.WriteLine(" Stores: ");
                
                        foreach(Storefront storefront in AllStores){
                            Console.WriteLine($"Name: {storefront.Name} \nAddress {storefront.Address}" );
                        }
                }
               break;

               case "2":
                    Console.WriteLine(" make a store ");
                    Console.WriteLine("Create a new Store:");
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Address: ");
                    string address = Console.ReadLine();

                    Storefront newStore = new Storefront {
                        Name = name,
                        Address =address,
                    };
                    _BL.AddStore(newStore);
                    
               break; 

               case "x":
               break;
            }

            break;

            // case "2":
            //         Console.WriteLine("view order history");
            //         Console.WriteLine("pick a store");
            //         Console.WriteLine(" [x] exit  ");
            //         string historychoice = Console.ReadLine();
            //         switch(historychoice )
            //         {
            //         case "1":
            //         Console.WriteLine(" Northstore history ");
            //         break;

            //         case "2":
            //         Console.WriteLine(" Southstore history ");
            //         break; 

            //         case "x":
            //         break;
            //         }
            //break;

        case "x":
        managerExit = true;
        break;
        }
    }
    new MainMenu().Start();
}

public void manager(){
        //this checks the manager password 
        bool Mexit= false;
        
        while(Mexit == false){
        string mPass = "1234";
        string Muser = "manager";


        Console.WriteLine("enter username");
        var managerUser = Console.ReadLine();
        System.Console.WriteLine("enter password");
        var managerPass = Console.ReadLine();
        
        if(mPass==managerPass && Muser == managerUser){

            Console.WriteLine("you got it");
            StartManager();
            Mexit = true;
        }
        else{
            Console.WriteLine("try again");
        }
     }
    
        
    } 

    public void ManageInventory(){




    }

    public void OrderHistory(){

        


    }
}

