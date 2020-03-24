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

        public static void addItem(string name, double price)
        {
            RestaurantProduct product = new RestaurantProduct(name, price);
            ProductList.productList.Add(product);            
        }
        
        public static void removeItem(string delName)
        {
            for (int i = 0; i < ProductList.productList.Count; i++)
            { 
                if (ProductList.productList[i].getProductInfo().Item1 == delName)
                {
                    ProductList.productList.RemoveAt(i);
                }                
            }
        }

        private static void Display()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Products:");
            for (int i = 0; i < ProductList.productList.Count; i++)
            {
                Console.WriteLine(ProductList.productList[i].getProductInfo().Item1 + "    €" + ProductList.productList[i].getProductInfo().Item2.ToString("0.00"));
            }
        }


        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Please input the desired action: ");
                Console.WriteLine("[1] Show current list of products");
                Console.WriteLine("[2] Add a product to the list");
                Console.WriteLine("[3] Remove a product from the list");
                Console.WriteLine("[4] Exit the menu");

                int operation = Convert.ToInt32(Console.ReadLine());
                if (operation == 1)
                {
                    Display();
                }
                else if (operation == 2)
                {
                    Console.WriteLine("Please fill in the name of the product.");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please fill in the price of the product.");
                    double price = double.Parse(Console.ReadLine());

                    addItem(name, price);
                }
                else if (operation == 3)
                {
                    Console.WriteLine("Please fill in the name of the product you wish to remove (Case Sensitive).");
                    string itemName = Console.ReadLine();

                    removeItem(itemName);
                }
                else if (operation == 4)
                {
                    //CinemaConsole.Exit();

                }
                else
                {
                    Console.WriteLine("Invalid Input. Please try again.");
                }
            }
        }
    }
}