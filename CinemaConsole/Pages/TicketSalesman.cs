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
            Console.WriteLine("All reservations:\nName:      E-mail:        Movie:      Date&Time:");
            for (int i = 0; i < ReservationList.reservationList.Count; i++)
            {
                Console.WriteLine(ReservationList.reservationList[i].getReservationInfo().Item3 + "  " + ReservationList.reservationList[i].getReservationInfo().Item4 + "  " + ReservationList.reservationList[i].getReservationInfo().Item1 + "  " + ReservationList.reservationList[i].getReservationInfo().Item2);
            }
        }


        public static void GetMovieDates(Movies movie)
        {
            Console.WriteLine("Movie selected: " + movie.getMovieInfo().Item2);
            string title = movie.getMovieInfo().Item2;

            foreach (DateTimeHall date in movie.DateTimeHallsList)
            {
                Console.WriteLine("[" + date.getHallInfo().Item1 + "] " + date.getHallInfo().Item2 + "      " + date.getHallInfo().Item3);
            }
        }

        public static void removeReservation(string delName)
        {
            for (int i = 0; i < ReservationList.reservationList.Count; i++)
            {
                if (ReservationList.reservationList[i].getReservationInfo().Item3 == delName)
                {
                    ReservationList.reservationList.RemoveAt(i);
                }
            }
        }


        public static void AddReservation()
        {
            Console.WriteLine("Please make a movie choice");

            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
            }

            string movieTitle = Console.ReadLine();

            Console.WriteLine("Please make a date and time choice");


            foreach (Movies aMovie in MovieList.movieList)
            {
                if (movieTitle == aMovie.getMovieInfo().Item1.ToString())
                {
                    GetMovieDates(aMovie);
                    break;
                }
            }


            string date_time = Console.ReadLine();

            Console.WriteLine("Please fill in the customer name:");
            string name = Console.ReadLine();
            Console.WriteLine("Please fill in the customer e-mail:");
            string email = Console.ReadLine();

            addReservation(movieTitle, date_time, name, email);

        }

        public static void addReservation(string movieTitle, string date_time, string name, string email)
        {
            TicketReservations reservations = new TicketReservations(movieTitle, date_time, name, email);
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

                    removeReservation(customerName);
                }
                else if (TicketSalesmanOption == "4")
                {
                    break;
                }
            }
        }
    }
}