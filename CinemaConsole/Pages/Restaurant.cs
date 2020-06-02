using System;
using System.Linq;
using CinemaConsole.Data;

namespace CinemaConsole.Pages.Restaurant
{
    public class Restaurant
    {
        /// <summary>
        /// Allows the retailer to edit a product
        /// </summary>
        /// <param name="productID">The ID of the product to edit</param>
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
                if (operation == "1")
                {
                    Console.Clear();
                    try
                    {
                        CD.ShowProductItem(productID, 1);
                        Console.WriteLine("\nPlease enter the new name of the product. If you wish to return to the menu, please enter [exit] instead.");
                        string inputName = Console.ReadLine().ToLower();                        
                        Console.WriteLine(" ");
                        if (inputName == "exit")
                        {
                            Console.Clear();
                            
                        }
                        else
                        {
                            string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                            CD.UpdateProduct(productID, newName);
                            CD.DisplayProducts();
                            break;
                        }
                    }
                    catch (FormatException f)
                    {
                        SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                        Console.WriteLine("Press [enter] to continue.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }

                //Edits the price of the selected product.
                else if (operation == "2")
                {
                    Console.Clear();
                    try
                    {
                        double example = 5.50;
                        CD.ShowProductItem(productID, 2);
                        Console.WriteLine("\nPlease enter the new price of the product in euro's. (e.g. " + example.ToString("0.00") + ") or write [exit] to go back.");
                        string input = Console.ReadLine().ToLower();
                        if (input == "exit")
                        {
                            Console.Clear();
                            
                        }
                        else
                        {
                            string temp = input.Replace(',', '.');
                            double newPrice = double.Parse(temp, System.Globalization.CultureInfo.InvariantCulture);
                            Console.WriteLine(" ");

                            CD.UpdateProduct(productID, "", newPrice);
                            CD.DisplayProducts();
                            break;
                        }                        
                    }
                    catch (FormatException f)
                    {
                        SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                        Console.WriteLine("Press [enter] to continue.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }

                //Edits both the name and price of the selected product. 
                //Also forces the first letter of the name to be upper case.
                else if (operation == "3")
                {
                    Console.Clear();
                    try
                    {
                        CD.ShowProductItem(productID, 1);
                        Console.WriteLine("\nPlease enter the new name of the product or write [exit] to go back.");
                        string inputName = Console.ReadLine().ToLower();
                        if(inputName == "exit")
                        {
                            Console.Clear();
                            
                        }
                        else
                        {
                            string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                            double example = 5.50;
                            CD.ShowProductItem(productID, 2);
                            Console.WriteLine($"\nPlease enter the new price of the product in euro's. (e.g. " + example.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ") or write [exit] to go back.");
                            string inputprice = Console.ReadLine().ToLower();
                            if(inputprice == "exit")
                            {
                                Console.Clear();
                            }
                            else
                            {
                                string temp = inputprice.Replace(',', '.');
                                double newPrice = double.Parse(temp, System.Globalization.CultureInfo.InvariantCulture);
                                Console.WriteLine(" ");

                                CD.UpdateProduct(productID, newName, newPrice);
                                CD.DisplayProducts();
                                break;
                            }
                        }                   
                    }
                    catch (FormatException f)
                    {
                        SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                        Console.WriteLine("Press [enter] to continue.");
                        Console.ReadLine();
                        Console.Clear();
                    }
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
                    SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                    Console.WriteLine("Press [enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }

            }
            
        }

        /// <summary>
        /// Add a product
        /// </summary>
        public static void AddProduct()
        {
            ChangeData CD = new ChangeData();
            ShowData SD = new ShowData();
            while (true)
            {
                Console.Clear();
                try
                {
                    //Requests the name of the product to be added.
                    Console.WriteLine("Please fill in the name of the product or write [exit] to go back to the menu.");
                    string inputName = Console.ReadLine();
                    if (inputName == "exit")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        string name = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                        Console.WriteLine(" ");
                        double example = 5.50;

                        //Requests the price of the product to be added.
                        Console.WriteLine("Please fill in the price of the product in euro's (e.g. " + example.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ") or write [exit] to go back to the menu.");
                        string inputPrice = Console.ReadLine();
                        if (inputPrice == "exit")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            string temp = inputPrice.Replace(',', '.');
                            double price = double.Parse(temp, System.Globalization.CultureInfo.InvariantCulture);
                            Console.WriteLine(" ");

                            CD.CreateProduct(name, price);
                            break;
                        }
                    }
                }
                catch (FormatException f)
                {
                    SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                    Console.WriteLine("Press [enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Remove a product
        /// </summary>
        public static void RemoveProduct()
        {
            ChangeData CD = new ChangeData();
            ShowData SD = new ShowData();
            while (true)
            {
                Console.Clear();
                CD.DisplayProducts();
                try
                {
                    Console.WriteLine("Please fill in the ID of the product you wish to remove or write [exit] to go back to the menu.");
                    string input = Console.ReadLine();
                    if (input == "exit")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        int itemID = Int32.Parse(input);
                        Console.Clear();

                        if (CD.checkIfPExists(itemID))
                        {
                            CD.DisplayProducts();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"\nAre you sure you wish to remove the item with ID {input}? This cannot be undone!\n");
                            Console.ResetColor();
                            Console.WriteLine("[1] Yes");
                            Console.WriteLine("[2] No");
                            string response = Console.ReadLine();
                            if (response == "1")
                            {
                                CD.DeleteProduct(itemID);
                                Console.Clear();
                                CD.DisplayProducts();
                                break;
                            }
                            else if (response == "2")
                            {

                            }
                            else
                            {
                                SD.ClearAndErrorMessage("\nInvalid Input. Please try again.");
                                Console.WriteLine("Press [enter] to continue.");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nThe given ID does not exist. Please check the ID and try again.");
                            Console.ResetColor();
                            Console.WriteLine("\nPress [enter] to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }

                    }
                }
                catch (FormatException f)
                {
                    SD.ClearAndErrorMessage("\nInvalid Input. Please try again.");
                    Console.WriteLine("Press [enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Edit a product
        /// </summary>
        public static void EditProduct()
        {
            ChangeData CD = new ChangeData();
            ShowData SD = new ShowData();
            Console.Clear();
            while (true)
            {
                try
                {
                    CD.DisplayProducts();
                    //Requests the name of the product to be edited.
                    Console.WriteLine("\nPlease fill in the ID of the product you wish to edit or write [exit] to go back to the menu.");
                    string input = Console.ReadLine();
                    if (input == "exit")
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        if (CD.checkIfPExists(Int32.Parse(input)))
                        {
                            int itemID = Int32.Parse(input);
                            Console.WriteLine(" ");

                            //Calls the editItem function and enters the ID of the product given earlier.
                            EditItem(itemID);
                            break;
                        }
                        else
                        {
                            SD.ClearAndErrorMessage("\nID does not exist. Please try again.");
                            Console.WriteLine("Press [enter] to continue.");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                }
                catch (FormatException f)
                {
                    SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                    Console.WriteLine("Press[enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// The menu of the restaurant
        /// </summary>
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
                Console.WriteLine("[5] Show the amount of movie reservations.");
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
                //Calls the AddProduct function.
                else if (operation == "2")
                {
                    AddProduct();
                }
                //Calls the RemoveProduct function.
                else if (operation == "3")
                {
                    RemoveProduct();
                }

                //Calls the EditProduct function.
                else if (operation == "4")
                {
                    EditProduct();
                }

                else if(operation == "5")
                {
                    Console.Clear();
                    CD.ReservationAmount();
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
                    Console.WriteLine("Press[enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}