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

            /*
            Console.WriteLine("\n[1] Search on name\n[2] Search on ticket number\n[3] Search on movie, time and date");
            string SearchOption = Console.ReadLine();
            
            if (SearchOption == "1")
            {
            }

            else if (SearchOption == "2")
            {
                //Search on ticketnumber
                Console.WriteLine("\nPlease enter the ticketnumber");
                string ticketnumber = Console.ReadLine();

                if (ReservationList.reservationList.Count != 0)
                {
                    foreach (TicketInfo ticket in ReservationList.reservationList)
                    {
                        if (ticket.GetTicketInfo().Item1.Item4 == ticketnumber)
                        {
                            Customer.Customer.Overview(ticket);
                            Console.WriteLine("\nTicketnumber: " + ticket.GetTicketInfo().Item1.Item4 + "\nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("\nThere were no results found with ticketnumber: " + ticketnumber + "\nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("\nThere were no results found with ticketnumber: " + ticketnumber + "\nPress enter to go back to the menu");
                    Console.ReadLine();
                }
            }

            //Search on movie/time/date
            else if (SearchOption == "3")
            {
                Console.WriteLine("\nPlease enter the movie");
                string movie = Console.ReadLine();

                Console.WriteLine("\nPlease enter the time (12:00)");
                string time = Console.ReadLine();

                Console.WriteLine("\nPlease enter the date (12/04/2020)");
                string date = Console.ReadLine();

                string DT = date + " " + time;

                if (ReservationList.reservationList.Count != 0)
                {
                    foreach (TicketInfo ticket in ReservationList.reservationList)
                    {
                        string DTT = ticket.GetTicketInfo().Item6.ToString("dd/MM/yyyy HH:mm");

                        if (DTT == DT && movie == ticket.GetTicketInfo().Item1.Item3)
                        {
                            Customer.Customer.Overview(ticket);
                            Console.WriteLine("\nTicketnumber: " + ticket.GetTicketInfo().Item1.Item4 + "\nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("\nThere were no results found \nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("\nThere were no results found \nPress enter to go back to the menu");
                    Console.ReadLine();
                }
            }*/
        }

        // This let the ticket salesman remove/cancel reservations. You have to type in the ticketnumber to remove the reservation.
        public static void RemoveReservation()
        {
            Console.WriteLine("\nPlease enter the ticketnumber of the reservation you want to remove:");
            string Cticketnumber = Console.ReadLine();

            foreach (TicketInfo ticket in ReservationList.reservationList)
            {
                if (ticket.GetTicketInfo().Item1.Item4 == Cticketnumber)
                {
                    Customer.Customer.Overview(ticket);
                    Console.WriteLine("\nTicketnumber: " + ticket.GetTicketInfo().Item1.Item4);
                    while (true)
                    {
                        Console.WriteLine("\nDo you really want to remove this reservation?\n[1] Remove reservation\n[2] Cancel");
                        string Coption = Console.ReadLine();

                        if (Coption == "1")
                        {
                            ReservationList.reservationList.Remove(ticket);
                            Console.WriteLine("\nReservation removed. Press enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }

                        else if (Coption == "2")
                        {
                            break;
                        }
                    }
                    break;
                }
                
                else
                {
                    Console.WriteLine("\nThere were no results found with ticketnumber: " + Cticketnumber + "\nPress enter to go back to the menu");
                    Console.ReadLine();
                    break;
                }
            }
        }

        // Ticketsalesman able to select a movie and see all the movie informarion.
        public static void MovieInfo()
        {
            string whichMovie;
            string CustomerTimeOption;

            while (true)
            {
                // convert movielist count to a string
                Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");

                Customer.Customer.display();

                string line = Console.ReadLine();

                // check if user wants to go back 
                if (line == "exit")
                {
                    break;
                }
                else if (line == "menu")
                {
                    Restaurant.Restaurant.Display();
                }

                ShowData ShowMovieByInfo = new ShowData();

                // this will return the movie details for the number you entered
                Tuple<string,string> movieInfo = ShowMovieByInfo.ShowMovieByID(line);
                whichMovie = movieInfo.Item1;
                Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                while (true)
                {
                    CustomerTimeOption = Console.ReadLine();
                    // this will return the movie times for the movie you entered
                    //ShowMovieByInfo.ShowTimesByMovieID(whichMovie, CustomerTimeOption);
                    if (CustomerTimeOption == "1")
                    {
                        Tuple<List<DateTime>, List<int>, List<int>> date = Customer.Customer.showTime(whichMovie);
                        while (true)
                        {
                            string CustomerReserve = Customer.Customer.selectTime(date);

                            if (CustomerReserve == "exit")
                            {
                                break;
                            }

                            else
                            {
                                Tuple<Tuple<int, int, int, int>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = Customer.Customer.hallSeatInfo(CustomerReserve, date);

                                Customer.Customer.showHall(hallseatInfo.Item1, hallseatInfo.Item2);
                            }
                        }

                        break;
                    }
                    else if(CustomerTimeOption == "exit")
                    {
                        break;
                    }
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
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action:\n[1] Search for reservation.\n[2] Add reservation.\n[3] Remove reservation.\n[4] Show movie information\n[exit] Exit the program.");
                string TicketSalesmanOption = Console.ReadLine();

                if (TicketSalesmanOption == "1")
                {
                    Display();
                }

                else if (TicketSalesmanOption == "2")
                {
                    AddReservation();
                }

                else if (TicketSalesmanOption == "3")
                {
                    RemoveReservation();
                }

                if (TicketSalesmanOption == "4")
                {
                    MovieInfo();
                }

                else if (TicketSalesmanOption == "exit")
                {
                    break;
                }
            }
        }
    }
}