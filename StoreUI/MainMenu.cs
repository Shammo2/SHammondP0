using System;
using Models;
namespace UI;
public class MainMenu {

    bool exit = false;
    bool signedin = false;
    public void Start() {

    while(!exit){
        if(signedin == false){

        Console.WriteLine("[1] sign in");
        Console.WriteLine("[2] create account");
        Console.WriteLine("[3] manager login ");
        string signinput= Console.ReadLine();
                    
                    switch(signinput){
                    case "1":
                        Console.WriteLine("You entered: Sign in");
                        new Customer().signin();
                        signedin = true;
                    break;
                    
                    case "2":
                        Console.WriteLine("You entered: Create Account");
                        new Customer().signup();
                        signedin = true;
                        
                    break;

                    case "3":
                    Console.WriteLine(" you entered: manager login ");
                    new ManagerUI().manager();

                    break;


                    }
        }

                Console.WriteLine("[1] Place Order (checkout");
                Console.WriteLine("[2] View stores and inventory");
                Console.WriteLine("[x] Exit");
                string menuinput = Console.ReadLine();
                
                switch(menuinput){

                        case "1":
                        Console.WriteLine("You entered: Place Order");
                        new orderplacement().placeorder();
                        break;

                        case "2":
                        Console.WriteLine("You entered: View stores and inventory");
                        break;

                        case "x":
                        exit = true;
                        break;

                }
    
        }
        Console.WriteLine("goodbye");
    }
}



