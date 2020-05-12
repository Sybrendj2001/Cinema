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
                if (operation == "1")
                {
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("\nPlease enter the new name of the product. If you wish to return to the menu, please enter [exit] instead.");
                        string inputName = Console.ReadLine();
                        string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                        Console.WriteLine(" ");
                        if (newName == "Exit")
                        {
                            break;
                        }
                        else
                        {
                            CD.UpdateProduct(productID, newName);
                            CD.DisplayProducts();
                            break;
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

                //Edits the price of the selected product.
                else if (operation == "2")
                {
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("\nPlease enter the new price of the product in euro's. Example: (5,00).");
                        double newPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine(" ");

                        CD.UpdateProduct(productID, "", newPrice);
                        CD.DisplayProducts();
                        break;
                    }
                    catch (FormatException f)
                    {
                        SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                        Console.WriteLine("Press[enter] to continue.");
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
                        Console.WriteLine("\nPlease enter the new name of the product.");
                        string inputName = Console.ReadLine();
                        string newName = inputName.First().ToString().ToUpper() + inputName.Substring(1);
                        Console.WriteLine("\nPlease enter the new price of the product in euro's. Example: (5,00)");
                        double newPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine(" ");

                        CD.UpdateProduct(productID, newName, newPrice);
                        CD.DisplayProducts();
                        break;
                    }
                    catch (FormatException f)
                    {
                        SD.ClearAndErrorMessage("Invalid Input. Please try again.");
                        Console.WriteLine("Press[enter] to continue.");
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
                    Console.WriteLine("Press[enter] to continue.");
                    Console.ReadLine();
                    Console.Clear();
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
                //Calls the addItem function.
                else if (operation == "2")
                {
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

                                //Requests the price of the product to be added.
                                Console.WriteLine("Please fill in the price of the product in euro's (Example: 5,00) or write [exit] to go back to the menu.");
                                string inputPrice = Console.ReadLine();
                                if (inputPrice == "exit")
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    double price = double.Parse(inputPrice);
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
                //Calls the removeItem function.
                else if (operation == "3")
                {
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
                                CD.DisplayProducts();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write($"\nAre you sure you wish to remove the item with ID {input}? [yes/no] This cannot be undone!");
                                Console.ResetColor();
                                string response = Console.ReadLine().ToLower();
                                if (response == "yes" || response == "y")
                                {                                    
                                    CD.DeleteProduct(itemID);
                                    break;
                                }
                                else if(response == "no" || response == "n")
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

                //Calls the EditItem function.
                else if(operation == "4")
                {
                    Console.Clear();
                    CD.DisplayProducts();
                    while (true)
                    {
                        try
                        {
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
                                    SD.ClearAndErrorMessage("ID does not exist. Please try again.");
                                    Console.WriteLine("Press[enter] to continue.");
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