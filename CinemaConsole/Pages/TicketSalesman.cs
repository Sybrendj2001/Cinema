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
        // Fake tickets
        public static void Tickets()
        {
            TicketInfo ticket1 = new TicketInfo("Mark van het Hof", "mark@gmailo.com", 5, 4, 1, 15.00, "10/10/2020 20:00", "Transformers", 2);
            ReservationList.reservationList.Add(ticket1);

            TicketInfo ticket2 = new TicketInfo("Tim Boersma", "tim@gmailo.com", 7, 6, 1, 15.00, "21/10/2020 20:12", "Good day to die hard", 1);
            ReservationList.reservationList.Add(ticket2);

            TicketInfo ticket3 = new TicketInfo("Johan hhhh", "johan@gmailo.com", 8, 4, 1, 15.00, "21/10/2020 20:12", "Skyfall", 2);
            ReservationList.reservationList.Add(ticket3);
        }

        // Ticketsalesman able to search to on customer name or ticketnumber or movie
        private static void Display()
        {
            Console.WriteLine("[1] Search on name\n[2] Search on ticket number\n[3] Search on movie, time and date");
            string SearchOption = Console.ReadLine();

            if (SearchOption == "1")
            {
                Console.WriteLine("Please enter the customer full name");
                string name = Console.ReadLine();

                if (ReservationList.reservationList.Count != 0)
                {
                    foreach (TicketInfo ticket in ReservationList.reservationList)
                    {
                        if (ticket.GetTicketInfo().Item1.Item1 == name)
                        {
                            Customer.Customer.Overview(ticket);
                            Console.WriteLine("Ticketnumber: " + ticket.GetTicketInfo().Item1.Item4 + "\nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("\nThere were no results found with name: " + name + "\nPress enter to go back to the menu");
                            Console.ReadLine();
                            break;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("\nThere were no results found with name: " + name + "\nPress enter to go back to the menu");
                    Console.ReadLine();
                }
            }

            else if (SearchOption == "2")
            {
                Console.WriteLine("Please enter the ticketnumber");
                string ticketnumber = Console.ReadLine();

                if (ReservationList.reservationList.Count != 0)
                {
                    foreach (TicketInfo ticket in ReservationList.reservationList)
                    {
                        if (ticket.GetTicketInfo().Item1.Item4 == ticketnumber)
                        {
                            Customer.Customer.Overview(ticket);
                            Console.WriteLine("Ticketnumber: " + ticket.GetTicketInfo().Item1.Item4 + "\nPress enter to go back to the menu");
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

            else if (SearchOption == "3")
            {
                Console.WriteLine("Please enter the movie");
                string movie = Console.ReadLine();

                Console.WriteLine("Please enter the time (12:00)");
                string time = Console.ReadLine();

                Console.WriteLine("Please enter the date (12/04/2020)");
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
                            Console.WriteLine("Ticketnumber: " + ticket.GetTicketInfo().Item1.Item4 + "\nPress enter to go back to the menu");
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
            }
        }

        // This let the ticket salesman  remove/cancel reservations.
        public static void RemoveReservation()
        {
            Console.WriteLine("Please enter the ticketnumber of the reservation you want to remove:");
            string Cticketnumber = Console.ReadLine();

            foreach (TicketInfo ticket in ReservationList.reservationList)
            {
                if (ticket.GetTicketInfo().Item1.Item4 == Cticketnumber)
                {
                    Customer.Customer.Overview(ticket);
                    Console.WriteLine("Ticketnumber: " + ticket.GetTicketInfo().Item1.Item4);

                    while (true)
                    {
                        Console.WriteLine("\nDo you really want to remove this reservation?\n[1] Remove reservation\n[2] Cancel");
                        string Coption = Console.ReadLine();

                        if (Coption == "1")
                        {
                            ReservationList.reservationList.Remove(ticket);
                            Console.WriteLine("Reservation removed. Press enter to go back to the menu");
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
            string line = "";
            while (true)
            {
                Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");
                Customer.Customer.display();
                line = Console.ReadLine();

                if (line == "exit")
                {
                    break;
                }

                else if (line == "menu")
                {
                    Restaurant.Restaurant.Display();
                }

                foreach (Movies aMovie in MovieList.movieList)
                {
                    if (line == aMovie.getMovieInfo().Item1.ToString())
                    {
                        Customer.Customer.GetMovieInfo(aMovie);
                    }
                }

                Console.WriteLine("\nPress enter to return to the movielist");
                Console.ReadLine();
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
                Console.WriteLine("\nPlease input the desired action:\n[1] Search for reservation.\n[2] Add reservation.\n[3] Remove reservation.\n[4] Show movie information\n[5] Exit the program.");
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

                else if (TicketSalesmanOption == "5")
                {
                    break;
                }
            }
        }
    }
}