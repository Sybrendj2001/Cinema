using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages.Admin;
using CinemaConsole.Pages.Customer;
using CinemaConsole.Pages.Restaurant;
using CinemaConsole.Pages.TicketSalesman;
using CinemaConsole.Pages;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant.SomeProducts();
            bool Running = true;
            string pageToBe = "";
            string toDo = "";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome to Cinema Keine Ahnung!");
            Console.ResetColor();

            while (Running)
            {
                if (pageToBe != "")
                {
                    toDo = pageToBe;
                    pageToBe = "";
                }
                else
                {

                    Console.WriteLine("\nPlease enter the number that stands before the option you want.\n[1] Login.\n[2] Show the movielist.\n[3] Contact info\n[4] Help\n[exit] Exit the program.");
                    toDo = Console.ReadLine();
                }
                switch (toDo)
                {
                    case "Admin":
                        Admin.Menu();
                        break;

                    case "Retailer":
                        Restaurant.Menu();
                        break;

                    case "Ticketsalesman":
                        TicketSalesman.Menu();
                        break;

                    case "1":
                        Login login = new Login();
                        login.Menu();
                        if(login.Function != "")
                        {
                            pageToBe = login.Function;
                            login.Function = "";
                        }
                        break;

                    case "2":
                        Customer.Menu();
                        break;

                    case "exit":
                        Running = false;
                        break;

                    case "3":
                        Console.WriteLine("\nAdres: Wijnhaven 99, 3011 WN Rotterdam\nPhone number: 010-794 4000\n\nOpening hours:\nMonday - Thursday: 09:00 - 21:00\nFriday: 09:00 - 01:00\nSaturday - Sunday: 12:00 - 02:00 \n\n");
                        break;

                    case "4":
                        Console.WriteLine("\nLogin: Log into your own page.\nMovielist: Show movielist.\nContact info: Shows the contact info of the cinema\nHelp: show help.\nPress enter to continue");
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("\nYou entered a wrong command");
                        break;
                }
            }
        }
    }
}
