using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data.Employee.ResataurantMenu;

namespace CinemaConsole.Pages
{
    class Restaurant
    {
        
        public void menu() 
        {
            Console.WriteLine("Please input the desired action: ");
            Console.WriteLine(" ");
            Console.WriteLine("[1] Show current list of products");
            Console.WriteLine(" ");
            Console.WriteLine("[2] Add a product to the list");
            Console.WriteLine(" ");
            Console.WriteLine("[3] Remove a product from the list");
            Console.WriteLine(" ");
            Console.WriteLine("[4] Exit the menu");

            int operation = Console.ReadLine();
            if(operation == 1)
			{

			}
            else if(operation == 2)
			{
                Console.WriteLine("Please fill in the name of the product.");
                string name = Console.ReadLine();
                Console.WriteLine("Please fill in the price of the product.");
                int price = Console.ReadLine();
                
                addProduct(name, price);
			}
            else if(operation == 3)
			{
                //Code here to remove a product from the list.
			}
            else if(operation == 4)
			{
                //Code here to exit application.
			}
			else
			{
                Console.WriteLine("Invalid Input. Please try again.");
			}
        }
        
        
        
        public void addProduct()
        {
            //Code here to add a product to the list.
        }

        public void removeProduct()
        {
            //Code here to remove a product to the list.
        }
    }
}