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

        void addItem(string name, double price)
        {

            RestaurantProduct product = new RestaurantProduct(name, price);
            ProductList.productList.Add(product);
            
        }
        
        void removeItem(string name)
        {
            //for (int i = 0; i < ProductList.productList.Count; i++)
            //{
            //    if (name == ProductList[i].Item1)
            //    {
            //        ProductList.RemoveAt(i);
            //    }
            //}
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

            int operation = Convert.ToInt32(Console.ReadLine());
            if (operation == 1)
            {
                //RestaurantMenu.printList();
            }
            else if (operation == 2)
            {
                Console.WriteLine("Please fill in the name of the product.");
                string name = Console.ReadLine();
                Console.WriteLine("Please fill in the price of the product.");
                double price = Convert.ToDouble(Console.ReadLine());

                addItem(name, price);
            }
            else if (operation == 3)
            {
                Console.WriteLine("Please fill in the name of the product you wish to remove (Case Sensitive).");
                string name = Console.ReadLine();

                //RestaurantProduct.removeItem(name);
            }
            else if (operation == 4)
            {
                //Application.Exit();

            }
            else
            {
                Console.WriteLine("Invalid Input. Please try again.");
            }
        }


    }

}