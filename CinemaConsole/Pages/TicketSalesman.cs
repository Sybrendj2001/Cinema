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
    public class TicketSalesman
    {
        /// <summary>
        /// Ticketsalesman able to search to on customer name or ticketnumber or movie
        /// </summary>
        private static void Display()
        {
            ShowData SD = new ShowData();
            SD.DisplayTickets();
        }

        /// <summary>
        /// This let the ticket salesman remove/cancel reservations
        /// </summary>
        public static void RemoveReservation()
        {
            ChangeData CD = new ChangeData();

            Console.WriteLine("\n[1] Remove using customers ticketnumber\n[2] Remove using customers email address\n[3] Remove using  name\n[exit] Exit to the menu");
            Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to see or action you want to do.");

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "exit")
                {
                    Console.Clear();
                    break;
                }
                else if (line == "1")
                {
                    Console.WriteLine("\nPlease enter the customers ticketnumber of the reservation you want to remove:");
                    string Cticketnumber = Console.ReadLine();
                    CD.DeleteReservationWithTicket(Cticketnumber);
                    break;
                }
                else if (line == "2")
                {
                    Console.WriteLine("\nPlease enter the customers email address of the reservation you want to remove:");
                    string emailaddress = Console.ReadLine();
                    CD.DeleteReservationWithEmail(emailaddress);
                    break;
                }
                else if (line == "3")
                {
                    Console.WriteLine("\nPlease enter the customers full name of the reservation you want to remove:");
                    string fullname = Console.ReadLine();
                    CD.DeleteReservationWithName(fullname);
                    break;
                }
            }
        }

        /// <summary>
        /// Ticketsalesman able to select a movie and see all the movie informarion
        /// </summary>
        public static void MovieInfo()
        {
            Console.Clear();
            Console.WriteLine("\nMovies:");
            ShowData SD = new ShowData();
            ChangeData CD = new ChangeData();
            List<int> MovieIDs = SD.ShowMovies();
            Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to see or action you want to do.");
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
                    Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to see or action you want to do.");
                    Console.WriteLine("\n[menu] Restaurant Menu");
                    Console.WriteLine("\n[exit] Exit to menu");
                }
                // extra check because a spacebar crashes the application
                else if (line != "" && line != " ")
                {
                    if (MovieIDs.Contains(Convert.ToInt32(line)))
                    {
                        // this will return the movie details for the number you entered
                        Tuple<string, string, string> movieInfo = SD.ShowMovieByID(line);
                        string whichMovie = movieInfo.Item1;

                        while (true)
                        {
                            Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                            string CustomerTimeOption = Console.ReadLine();
                            if (CustomerTimeOption == "1")
                            {
                                // this will return the movie times for the movie you entered
                                Tuple<List<DateTime>, List<int>, List<int>> dates = Customer.showTime(whichMovie);
                                string timeSelect = Customer.selectTime(dates, whichMovie);

                                if (timeSelect != "exit")
                                {
                                    Console.Clear();
                                    Console.WriteLine("");
                                    Tuple<Tuple<int, int, int, int, double, double, double>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = Customer.hallSeatInfo(timeSelect, dates);

                                    Customer.showHall(hallseatInfo.Item1, hallseatInfo.Item2);

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

        /// <summary>
        /// Menu with the options for the ticket salesman to choose from.
        /// </summary>
        public static void Menu()
        {
            ShowData SD = new ShowData();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action:\n[1] Search for reservation\n[2] Add reservation\n[3] Remove reservation\n[4] Show movie information\n[exit] Exit the program");
                string TicketSalesmanOption = Console.ReadLine();

                if (TicketSalesmanOption.Length > 5)
                {
                    SD.ClearAndErrorMessage("Your input is too big");
                }
                else if (TicketSalesmanOption == "1")
                {
                    Display();
                    Console.Clear();
                }

                else if (TicketSalesmanOption == "2")
                {
                    Customer.Menu();
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