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

        //Allows the retailer to edit items on the list of products.
        public static void EditItem(int productID)
        {
            ChangeData CD = new ChangeData();
            ShowData SD = new ShowData();
            Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("\nPlease enter the new name of the product.");
                    string inputName = Console.ReadLine();
                    string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, newName);
                    Console.Clear();
                    break;
                }

                //Edits the price of the selected product.
                //Does so by creating a new product with the old name and new price, and removing the old product from the list.
                //Places the new product in place of the old one afterwards.
                else if (operation == "2")
                {
                    Console.Clear();
                    Console.WriteLine("\nPlease enter the new price of the product in euro's.");
                    double newPrice = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, "", newPrice);
                    Console.Clear();
                    break;
                }

                //Edits both the name and price of the selected product. 
                //Does so by creating a new product with the new name and old price, and removing the old product from the list.
                //Places the new product in place of the old one afterwards.
                //Also forces the first letter of the name to be upper case.
                else if (operation == "3")
                {
                    Console.Clear();
                    Console.WriteLine("\nPlease enter the new name of the product.");
                    string inputName = Console.ReadLine();
                    string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine("\nPlease enter the new price of the product in euro's.");
                    double newPrice = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    CD.UpdateProduct(productID, newName, newPrice);
                    Console.Clear();
                    break;
                }

                //Exits the currrent menu.
                else if (operation == "exit")
                {
                    Console.Clear();
                    break;
                }

                //Gives out a warning message when invalid input is detected.
                else
                {
                    SD.ClearAndErrorMessage("\nInvalid input. Please try again.\n");
                }

            }
            
        }

        //Presents a menu with options to choose from.
        public static void Menu()
        {
            ChangeData CD = new ChangeData();
            ShowData SD = new ShowData();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action: ");
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
                    Console.Clear();
                    CD.DisplayProducts();
                }
                //Calls the addItem function.
                else if (operation == "2")
                {
                    Console.Clear();
                    //Requests the name of the product to be added.
                    Console.WriteLine("Please fill in the name of the product.");
                    string inputName = Console.ReadLine();
                    string name = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                    Console.WriteLine(" ");

                    //Requests the price of the product to be added.
                    Console.WriteLine("Please fill in the price of the product in euro's.");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    CD.CreateProduct(name, price);
                }
                //Calls the removeItem function.
                else if (operation == "3")
                {
                    Console.Clear();
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
                    Console.Clear();
                    CD.DisplayProducts();
                    //Requests the name of the product to be edited.
                    Console.WriteLine("\nPlease fill in the ID of the product you wish to edit.");
                    int itemID = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(" ");

                    //Calls the editItem function and enters the ID of the product given earlier.
                    EditItem(itemID);
                }
                //Exits out of this menu.
                else if (operation == "exit")
                {
                    Console.Clear();
                    break;
                }
                //Display error message when input is considered invalid.
                else
                {
                    SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                }
            }
        }
    }
}