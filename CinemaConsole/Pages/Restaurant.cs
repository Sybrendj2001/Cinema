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
        public static void SomeProducts()
        {
            Restaurant.AddItem("Cola", 2.50);
            Restaurant.AddItem("Popcorn", 3.50);
        }

        //Allows the retailer to add new items to the list of products.
        //Information on the products consists of the name and price of the product.
        public static void AddItem(string name, double price)
        {
            RestaurantProduct product = new RestaurantProduct(name, price);
            ProductList.productList.Add(product);
        }

        //Allows the retailer to remove items from the list of products.
        //Curently requires you to enter the name of the product.
        public static void RemoveItem(int delID)
        {
            for (int i = 0; i < ProductList.productList.Count; i++)
            {
                if (ProductList.productList[i].getProductInfo().Item1.Equals(delID))
                {
                    ProductList.productList[i] = null;
                }
            }
        }

        public static void EditItem(int productID)
        {
            for (int i = 0; i < ProductList.productList.Count; i++)
            {
                if (ProductList.productList[i].getProductInfo().Item1.Equals(productID))
                {
                    Console.WriteLine("Please select which part you wish to edit:");
                    Console.WriteLine("[1] Edit the name");
                    Console.WriteLine("[2] Edit the price");
                    Console.WriteLine("[3] Edit both");
                    Console.WriteLine("[exit] Exit to the last menu");
                    Console.WriteLine(" ");
                    string operation = Console.ReadLine();
                    if(operation == "1")
                    {
                        Console.WriteLine("Please enter the new name of the product.");
                        string newName = Console.ReadLine();

                        RestaurantProduct product = new RestaurantProduct(newName, ProductList.productList[i].getProductInfo().Item3);


                        ProductList.productList[i] = product;
                    }
                    else if(operation == "2")
                    {

                    }
                    else if(operation == "3")
                    {

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }
        }

        //Displays a list of the items within the list of products.
        public static void Display()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Products:");
            for(int j = 0; j < ProductList.productList.Count+1; j++)
            {
                for (int i = 0; i < ProductList.productList.Count; i++)
                {
                    if (ProductList.productList[i] != null)
                    {
                        if (j == ProductList.productList[i].getProductInfo().Item1)
                        {
                            Console.WriteLine("[" + ProductList.productList[i].getProductInfo().Item1 + "] " + ProductList.productList[i].getProductInfo().Item2 + "    €" + ProductList.productList[i].getProductInfo().Item3.ToString("0.00"));
                        }
                    }                    
                }
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
                Console.WriteLine("[4] Edit an item on the list.");
                Console.WriteLine("[exit] Exit the menu");

                //Requests input and executes functions depending on the choice.
                string operation = Console.ReadLine();

                //Calls the Display function.
                if (operation == "1")
                {
                    Display();
                }
                //Calls the addItem function.
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
                    AddItem(name, price);
                    Restaurant.Display();
                    Console.WriteLine(ProductList.productList.Count());
                }
                //Calls the removeItem function.
                else if (operation == "3")
                {
                    Restaurant.Display();
                    //Requests the name of the product to be removed.
                    Console.WriteLine("Please fill in the ID of the product you wish to remove.");
                    int itemID = Int32.Parse(Console.ReadLine());

                    //Calls the removeItem function and enters the name of the product given earlier.
                    RemoveItem(itemID);
                }
                else if(operation == "4")
                {
                    //Requests the name of the product to be edited.
                    Console.WriteLine("Please fill in the ID of the product you wish to edit.");
                    int itemID = Int32.Parse(Console.ReadLine());

                    //Calls the editItem function and enters the ID of the product given earlier.
                    EditItem(itemID);
                }
                //Exits out of this menu.
                else if (operation == "exit")
                {
                    break;
                }
                //Display error message when input is considered invalid.
                else
                {
                    Console.WriteLine("Invalid Input. Please try again.");
                }
            }
        }
    }
}