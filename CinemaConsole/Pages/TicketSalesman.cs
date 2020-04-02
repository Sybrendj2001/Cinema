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

        // Shows the ticket salesman all reservations with details of the customer and the movie.
        private static void Display()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("All reservations:\nName: E-mail:  Movie: Date&Time:     Seats:");
            for (int i = 0; i < ReservationList.reservationList.Count; i++)
            {
                Console.WriteLine(ReservationList.reservationList[i].getReservationInfo().Item4 + "  " + ReservationList.reservationList[i].getReservationInfo().Item5 + "  " + ReservationList.reservationList[i].getReservationInfo().Item1 + "  " + ReservationList.reservationList[i].getReservationInfo().Item2 + "  " + ReservationList.reservationList[i].getReservationInfo().Item3);
            }
        }

        // This let the ticket salesman  remove/cancel reservations.
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
                string Pseats = "";
                int TheatherHall = 0;

                foreach (Movies aMovie in MovieList.movieList)
                {
                    if (line == aMovie.getMovieInfo().Item1.ToString())
                    {
                        MovieTitle = aMovie.getMovieInfo().Item2.ToString();
                       
                        Console.WriteLine(aMovie.getMovieInfo().Item2 + " (" + aMovie.getMovieInfo().Item3 + ")");

                        foreach (DateTimeHall date in aMovie.DateTimeHallsList)
                        {
                            Console.WriteLine("[" + date.getDateInfo().Item1 + "]       " + date.getDateInfo().Item2 + "      " + date.getDateInfo().Item3 +"    Theaterhall " +  date.getDateInfo().Item4.getHallInfo().Item2 );
                        }

                        string w = Console.ReadLine();

                        foreach (DateTimeHall date in aMovie.DateTimeHallsList)
                        {
                            if (w == date.getDateInfo().Item1.ToString())
                            {
                                MovieDT = date.getDateInfo().Item2 +" " + date.getDateInfo().Item3;
                                TheatherHall = date.getDateInfo().Item4.getHallInfo().Item2;
                            }
                        }

                        Tuple<int, int, int> AmountXY = Customer.Customer.SelectSeat(aMovie, aMovie.getMovieInfo().Item1);

                        if(AmountXY.Item1 == 0)
                        {
                            break;
                        }

                        else
                        {
                            //TicketInfo TI = new TicketInfo(name, AmountXY.Item2, AmountXY.Item3, 15.70, MovieDT, MovieTitle, TheatherHall);

                        }


                        Pseats = AmountXY.Item2.ToString() + "/" + AmountXY.Item3.ToString();
                    }
                }

                string movieTitle = MovieTitle;
                string date_time = MovieDT;
                string seats = Pseats;

                //Console.WriteLine("Please enter the amount of tickets:");
                //int ticketAmount = Convert.ToInt32(Console.ReadLine());


                Console.WriteLine("Please enter the customer name:");
                string name = Console.ReadLine();

                Console.WriteLine("Please enter the customer e-mail:");
                string email = Console.ReadLine();

                Console.WriteLine("Reservation saved");

                AddReservation(movieTitle, date_time, seats, name, email);
                break;
            }
        }

        // Add the reservation to the resservation list.
        public static void AddReservation(string movieTitle, string date_time, string seats, string name, string email)
        {
            TicketReservations reservations = new TicketReservations(movieTitle, date_time, seats, name, email);
            ReservationList.reservationList.Add(reservations);
        }

        // Menu with the options for the ticket salesman to choose from.
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nPlease input the desired action:\n[1] Show all reservations.\n[2] Add reservation.\n[3] Remove reservation.\n[4] Show movie information\n[5] Exit the program.");
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