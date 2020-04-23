using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Pages.TicketSalesman
{
    public class TicketSalesman : Employee
    {
        public TicketSalesman()
        {

        }

        // Ticketsalesman able to search to on customer name or ticketnumber or movie
        private static void Display()
        {
            ShowData DTicket = new ShowData();
            DTicket.DisplayTickets();
        }

        // This let the ticket salesman remove/cancel reservations. You have to type in the ticketnumber to remove the reservation.
        public static void RemoveReservation()
        {
            Console.WriteLine("\nPlease enter the ticketnumber of the reservation you want to remove:");
            string Cticketnumber = Console.ReadLine();

            ChangeData DeleteTicket = new ChangeData();
            DeleteTicket.DeleteReservation(Cticketnumber);
        }

        // Ticketsalesman able to select a movie and see all the movie informarion.
        public static void MovieInfo()
        {
            Console.Clear();
            Console.WriteLine("\nMovies:");
            ShowData SD = new ShowData();
            ChangeData CD = new ChangeData();
            List<int> MovieIDs = SD.ShowMovies();
            Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");
            Console.WriteLine("\n[menu] Restaurant Menu");
            Console.WriteLine("\n[exit] Exit to menu");

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "exit")
                {
                    Console.Clear();
                    break;
                }
                else if (line == "menu")
                {
                    CD.DisplayProducts();
                    Console.WriteLine("\nMovies:");
                    SD.ShowMovies();
                    Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");
                    Console.WriteLine("\n[menu] Restaurant Menu");
                    Console.WriteLine("\n[exit] Exit to menu");
                }
                // extra check because a spacebar crashes the application
                else if (line != "" && line != " ")
                {
                    if (MovieIDs.Contains(Convert.ToInt32(line)))
                    {
                        // this will return the movie details for the number you entered
                        Tuple<string, string> movieInfo = SD.ShowMovieByID(line);
                        string whichMovie = movieInfo.Item1;

                        while (true)
                        {
                            Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                            string CustomerTimeOption = Console.ReadLine();
                            if (CustomerTimeOption == "1")
                            {
                                // this will return the movie times for the movie you entered
                                Tuple<List<DateTime>, List<int>, List<int>> dates = Customer.Customer.showTime(whichMovie);
                                string timeSelect = Customer.Customer.selectTime(dates);

                                if (timeSelect != "exit")
                                {
                                    Console.Clear();
                                    Console.WriteLine("");
                                    Tuple<Tuple<int, int, int, int>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = Customer.Customer.hallSeatInfo(timeSelect, dates);

                                    Customer.Customer.showHall(hallseatInfo.Item1, hallseatInfo.Item2);

                                    Console.WriteLine("\nPress enter to continue");
                                    Console.ReadLine();
                                }
                                break;
                            }
                            else if (CustomerTimeOption == "exit")
                            {
                                break;
                            }
                            else
                            {
                                SD.ErrorMessage("\nPlease enter an option that exists");
                            }
                        }
                    }
                    else
                    {
                        SD.ErrorMessage("\nPlease enter an option that exists");
                    }
                    break;
                }
                else
                {
                    SD.ErrorMessage("\nPlease enter an option that exists");
                }
            }
        }
    

        // The the ticket salesman is able to make a reservation for customers. You can make a movie choice, pick a date and time, 
        // put in the amount of tickets, put in the contact information of the customer.
        public static void AddReservation()
        {
            Customer.Customer.Menu();
        }

        // Menu with the options for the ticket salesman to choose from.
        public static void Menu()
        {
            ShowData SD = new ShowData();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action:\n[1] Search for reservation.\n[2] Add reservation.\n[3] Remove reservation.\n[4] Show movie information\n[exit] Exit the program.");
                string TicketSalesmanOption = Console.ReadLine();

                if (TicketSalesmanOption == "1")
                {
                    Display();
                    Console.Clear();
                }

                else if (TicketSalesmanOption == "2")
                {
                    AddReservation();
                    Console.Clear();
                }

                else if (TicketSalesmanOption == "3")
                {
                    Console.Clear();
                    RemoveReservation();
                }

                else if (TicketSalesmanOption == "4")
                {
                    MovieInfo();
                    Console.Clear();
                }

                else if (TicketSalesmanOption == "exit")
                {
                    break;
                }
                else
                {
                    SD.ClearAndErrorMessage("\nThe option you entered does not exist");
                }
            }
        }
    }
}