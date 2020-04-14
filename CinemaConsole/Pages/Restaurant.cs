using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data.BackEnd;


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
                if (ProductList.productList[i] != null) {
                    if (ProductList.productList[i].getProductInfo().Item1.Equals(delID))
                    {
                        ProductList.productList[i] = null;
                    }
                }
            }
        }

        //Allows the retailer to edit items on the list of products.
        public static void EditItem(int productID)
        {
            ChangeData CD = new ChangeData();

            while (true)
            {
                Console.WriteLine("Please select which part you wish to edit:");
                Console.WriteLine("[1] Edit the name");
                Console.WriteLine("[2] Edit the price");
                Console.WriteLine("[3] Edit both");
                Console.WriteLine("[exit] Exit to the last menu");
                Console.WriteLine(" ");

                string operation = Console.ReadLine();

                //Edits the name of the selected product. Also forces the first letter of the name to be upper case.
                //Does so by creating a new product with the new name and old price, and removing the old product from the list.
                //Places the new product in place of the old one afterwards.
                if (operation == "1")
                {
                    Console.WriteLine("\nPlease enter the new name of the product.");
                    string inputName = Console.ReadLine();
                    string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, newName);
                    break;
                }

                //Edits the price of the selected product.
                //Does so by creating a new product with the old name and new price, and removing the old product from the list.
                //Places the new product in place of the old one afterwards.
                else if (operation == "2")
                {
                    Console.WriteLine("\nPlease enter the new price of the product in euro's.");
                    double newPrice = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, "", newPrice);
                    break;
                }

                //Edits both the name and price of the selected product. 
                //Does so by creating a new product with the new name and old price, and removing the old product from the list.
                //Places the new product in place of the old one afterwards.
                //Also forces the first letter of the name to be upper case.
                else if (operation == "3")
                {
                    Console.WriteLine("\nPlease enter the new name of the product.");
                    string inputName = Console.ReadLine();
                    string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine("\nPlease enter the new price of the product in euro's.");
                    double newPrice = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, newName, newPrice);
                    break;
                }

                //Exits the currrent menu.
                else if (operation == "exit")
                {
                    break;
                }

                //Gives out a warning message when invalid input is detected.
                else
                {
                    Console.WriteLine("\nInvalid input. Please try again.\n");
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
            Console.WriteLine(" ");
        }

        //Presents a menu with options to choose from.
        public static void Menu()
        {
            ChangeData CD = new ChangeData();
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
                Console.WriteLine(" ");

                //Calls the Display function.
                if (operation == "1")
                {
                    CD.DisplayProducts();
                }
                //Calls the addItem function.
                else if (operation == "2")
                {
                    //Requests the name of the product to be added.
                    Console.WriteLine("Please fill in the name of the product.");
                    string inputName = Console.ReadLine();
                    string name = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine(" ");

                    //Requests the price of the product to be added.
                    Console.WriteLine("Please fill in the price of the product in euro's.");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    int totalproducts = CD.TotalProducts();
                    CD.CreateProduct(totalproducts, name, price);
                }
                //Calls the removeItem function.
                else if (operation == "3")
                {
                    CD.DisplayProducts();
                    //Requests the name of the product to be removed.
                    Console.WriteLine("Please fill in the ID of the product you wish to remove.");
                    int itemID = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    //Calls the removeItem function and enters the name of the product given earlier.
                    CD.DeleteProduct(itemID);
                }
                else if(operation == "4")
                {
                    //Requests the name of the product to be edited.
                    Console.WriteLine("Please fill in the ID of the product you wish to edit.");
                    int itemID = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

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
                    Console.WriteLine(" ");
                }
            }
        }
    }
}