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

        //Adds some items to the productlist.
        //Function is only invoked when logging in.
        public static void someProducts()
        {
            Restaurant.addItem("Cola", 2.50);
            Restaurant.addItem("Popcorn", 3.50);
        }

        //Allows the retailer to add new items to the list of products.
        //Information on the products consists of the name and price of the product.
        public static void addItem(string name, double price)
        {
            RestaurantProduct product = new RestaurantProduct(name, price);
            ProductList.productList.Add(product);
        }

        //Allows the retailer to remove items from the list of products.
        //Curently requires you to enter the name of the product.
        public static void removeItem(string delName)
        {
            for (int i = 0; i < ProductList.productList.Count; i++)
            {
                if (ProductList.productList[i].getProductInfo().Item1.Equals(delName, StringComparison.OrdinalIgnoreCase))
                {
                    ProductList.productList.RemoveAt(i);
                }
            }
        }

        //Displays a list of he items within the list of products.
        public static void Display()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Products:");
            for (int i = 0; i < ProductList.productList.Count; i++)
            {
                Console.WriteLine(ProductList.productList[i].getProductInfo().Item1 + "    €" + ProductList.productList[i].getProductInfo().Item2.ToString("0.00"));
            }
        }

        //Presents a menu with options to choose from.
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Please input the desired action: ");
                Console.WriteLine("[1] Show current list of products");
                Console.WriteLine("[2] Add a product to the list");
                Console.WriteLine("[3] Remove a product from the list");
                Console.WriteLine("[exit] Exit the menu");

                //Requests input and executes functions depending on the choice.
                string operation = Console.ReadLine();

                //calls the Display function.
                if (operation == "1")
                {
                    Display();
                }
                else if (operation == "2")
                {
                    //Requests the name of the product to be added.
                    Console.WriteLine("Please fill in the name of the product.");
                    string inputName = Console.ReadLine();

                    //Requests the price of the product to be added.
                    Console.WriteLine("Please fill in the price of the product.");
                    double price = double.Parse(Console.ReadLine());

                    string name = inputName.First().ToString().ToUpper() + inputName.Substring(1);

                    //Calls the addItem function and enters the name and price given earlier.
                    addItem(name, price);
                }
                else if (operation == "3")
                {
                    Restaurant.Display();
                    //Requests the name of the product to be removed.
                    Console.WriteLine("Please fill in the name of the product you wish to remove.");
                    string itemName = Console.ReadLine();

                    //Calls the removeItem function and enters the name of the product given earlier.
                    removeItem(itemName);
                }
                else if (operation == "exit")
                {
                    //Exits to the former step.
                    break;
                }
                else
                {
                    //Display error message.
                    Console.WriteLine("Invalid Input. Please try again.");
                }
            }
        }
    }
}