﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages.Admin;
using CinemaConsole.Pages.Customer;
using CinemaConsole.Pages.Restaurant;
using CinemaConsole.Pages;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer.AddStuff();
            Restaurant.someProducts();
            bool Running = true;
            string pageToBe = "";
            string toDo = "";
            while (Running)
            {
                if (pageToBe != "")
                {
                    toDo = pageToBe;
                    pageToBe = "";
                    toDo.ToLower();
                }
                else
                {
                    Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Login.\n[2] Show the movielist.\n[3] Create ticket\n[4] Contact info\n[5] Help\n[exit] Exit the program.");
                    toDo = Console.ReadLine();
                }
                switch (toDo)
                {
                    case "admin":
                        Admin.Menu();
                        break;

                    case "retailer":
                        Restaurant.Menu();
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

                    case "3":
                        TicketInfo goIntoTicket = new TicketInfo("Sybren",3,3,13.00,DateTime.Now,"Thor","HALL2");
                        goIntoTicket.Menu();
                        break;

                    case "exit":
                        Running = false;
                        break;

                    case "4":
                        Console.WriteLine("\nAdres: Wijnhaven 99, 3011 WN Rotterdam\nPhone number: 010-794 4000\n\nOpening hours:\nMonday - Thursday: 12:00 - 21:00\nFriday: 12:00 - 01:00\nSaturday - Sunday: 12:00 - 02:00 \n\n");
                        break;

                    case "5":
                        Console.WriteLine("Help: show help.\nLogin: Log into your own page.\nMovielist: Show movielist.\nPress enter to continue");
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("You entered a wrong command");
                        break;
                }
            }
        }
    }
}
