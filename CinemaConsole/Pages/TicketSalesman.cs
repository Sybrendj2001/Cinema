using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data;
using CinemaConsole.Data.Employee;


namespace CinemaConsole.Pages.TicketSalesman
{
    public class TicketSalesman : Employee
    {
        public TicketSalesman()
        {

        }

        private static void Display()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("All reservations:\nName: E-mail: Ticket amount: Movie: Date&Time:");
            for (int i = 0; i < ReservationList.reservationList.Count; i++)
            {
                Console.WriteLine(ReservationList.reservationList[i].getReservationInfo().Item4 + "  " + ReservationList.reservationList[i].getReservationInfo().Item5 + "  " + ReservationList.reservationList[i].getReservationInfo().Item3 + "  " + ReservationList.reservationList[i].getReservationInfo().Item1 + "  " + ReservationList.reservationList[i].getReservationInfo().Item2);
            }
        }


        public static void RemoveReservation(string delName)
        {
            for (int i = 0; i < ReservationList.reservationList.Count; i++)
            {
                if (ReservationList.reservationList[i].getReservationInfo().Item4 == delName)
                {
                    ReservationList.reservationList.RemoveAt(i);
                }
            }
        }


        public static void AddReservation()
        {
            while (true)
            {
                Console.WriteLine("Please enter a movie choice");

                foreach (Movies movie in MovieList.movieList)
                {
                    Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                }


                string line = Console.ReadLine();

                Console.WriteLine("Please enter a date and time choice");
                string MovieTitle = "";
                string MovieDT = "";

                foreach (Movies aMovie in MovieList.movieList)
                {
                    if (line == aMovie.getMovieInfo().Item1.ToString())
                    {
                        MovieTitle = aMovie.getMovieInfo().Item2.ToString();

                        Console.WriteLine("[" + aMovie.getMovieInfo().Item1 + "]   " + aMovie.getMovieInfo().Item2 + " (" + aMovie.getMovieInfo().Item3 + ")");

                        foreach (DateTimeHall date in aMovie.DateTimeHallsList)
                        {
                            Console.WriteLine(date.getHallInfo().Item1 + "      " + date.getHallInfo().Item2 + "    Theaterhall " + date.getHallInfo().Item3);
                        }

                        string w = Console.ReadLine();

                        foreach (DateTimeHall date in aMovie.DateTimeHallsList)
                        {
                            if (w == date.getHallInfo().Item1.ToString())
                            {
                                MovieDT = date.getHallInfo().Item1 + "      " + date.getHallInfo().Item2 + "    Theaterhall " + date.getHallInfo().Item3;
                            }
                        }
                    }
                }
                
                string movieTitle = MovieTitle;
                string date_time = MovieDT;

                Console.WriteLine("Please enter the amount of tickets:");
                int ticketAmount = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter the customer name:");
                string name = Console.ReadLine();

                Console.WriteLine("Please enter the customer e-mail:");
                string email = Console.ReadLine();

                AddReservation(movieTitle, date_time, ticketAmount, name, email);
                break;
            }
        }

        public static void AddReservation(string movieTitle, string date_time, int ticketAmount, string name, string email)
        {
            TicketReservations reservations = new TicketReservations(movieTitle, date_time, ticketAmount, name, email);
            ReservationList.reservationList.Add(reservations);
        }

        public static void Menu()
        {
            Customer.Customer.AddStuff();
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action:\n[1] Show all reservations.\n[2] Add reservation.\n[3] Remove reservation.\n[4] Exit the program.");
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
                    Console.WriteLine("Please fill in the name of the customer you wish to remove (Case Sensitive).");
                    string customerName = Console.ReadLine();

                    RemoveReservation(customerName);
                }

                else if (TicketSalesmanOption == "4")
                {
                    break;
                }
            }
        }
    }
}