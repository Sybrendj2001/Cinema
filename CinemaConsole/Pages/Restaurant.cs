using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data.Employee;

namespace CinemaConsole.Pages.Restaurant
{
    public class Restaurant : Employee
    {
        public Restaurant()
		{

		}
        
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
                //RestaurantMenu.printList();
			}
            else if(operation == 2)
			{
                Console.WriteLine("Please fill in the name of the product.");
                string name = Console.ReadLine();
                Console.WriteLine("Please fill in the price of the product.");
                int price = Console.ReadLine();
                
                RestaurantMenu.addItem(name, price);
			}
            else if(operation == 3)
			{
                Console.WriteLine("Please fill in the name of the product you wish to remove (Case Sensitive).");
                string name = Console.ReadLine();

                RestaurantMenu.removeItem(name);
            }
            else if(operation == 4)
			{
                Application.Exit();

            }
			else
			{
                Console.WriteLine("Invalid Input. Please try again.");
			}
        }
        
        
    }

}