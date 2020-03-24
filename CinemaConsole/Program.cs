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
                    Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Login.\n[2] Show the movielist.\n[3] Create ticket\n[4] Exit the program.");
                    toDo = Console.ReadLine();
                    if (!int.TryParse(toDo, out _))
                    {
                        toDo = "somethingthatdoesntexist";
                    }
                }
                switch (toDo)
                {
                    case "admin":
                        Admin.Menu();
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

                    case "retailer":
                        Restaurant.Menu();
                        break;

                    case "help":
                        Console.WriteLine("Help: show help.\nLogin: Log into your own page.\nMovielist: Show movielist.");

                    case "3":
                        TicketInfo goIntoTicket = new TicketInfo("Sybren",3,3,13.00,DateTime.Now,"Thor","HALL2");
                        goIntoTicket.Menu();
                        break;

                    case "4":
                        Running = false;
                        break;

                    case "help":
                        Console.WriteLine("Help: show help.\nLogin: Log into your own page.\nMovielist: Show movielist.");
                        break;

                    default:
                        Console.WriteLine("You are writting a command wrong or the command doesn't exist yet");
                        break;
                }
            }
        }
    }
}
