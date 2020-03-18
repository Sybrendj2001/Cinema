using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.Employee;

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

            int operation = Console.ReadLine();
            if(operation == 1)
			{

			}
            else if(operation == 2)
			{

			}
            else if(operation == 3)
			{

			}
			else
			{
                Console.WriteLine("Invalid Input. Please try again.");
			}
        }
        
        
        
        public void addProduct()
        {
            
        }

        public void removeProduct()
        {

        }
    }
}