using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CinemaConsole.Data;
using CinemaConsole.Data.BackEnd;
using CinemaConsole.Data.Employee;
using CinemaConsole.Pages.Restaurant;


namespace CinemaConsole.Pages.Customer
{
    public class Customer
    {
        public static void display()
        {
            Console.WriteLine("\nMovies:");

            ShowData showMovieInfo = new ShowData();
            showMovieInfo.ShowMovies();

            // check if user wants to go back
            Console.WriteLine("\n[menu] Restaurant Menu");
            Console.WriteLine("\n[exit] Back to the menu.");
        }

        private static Tuple<string, string, string> Name()
        {
            Console.WriteLine("Please enter your first name");
            string first_name2 = Console.ReadLine();
            string first_name = first_name2.ToString().ToLower();

            Console.WriteLine("Please enter your last name");
            string last_name2 = Console.ReadLine();
            string last_name = last_name2.ToString().ToLower();

            Console.WriteLine("\nPlease enter your e-mail adress");
            string email;

            // loop to only accept a valid email
            while (true)
            {
                email = Console.ReadLine();

                if (IsValidEmail(email))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid e-mail adress");
                }
            }

            // creating a boolean to check with a system function if it returns an error
            bool IsValidEmail(string emailaddress)
            {
                Regex rx = new Regex(
                @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(email);
            }

            return Tuple.Create(first_name, last_name, email);
        }

        //Overview of the customers information and movie information.You can check all the information before you reserve the tickets.
        public static void Overview(TicketInfo ticket)
        {
            Tuple<Tuple<string, string, string, string>, int, int, int, double, DateTime, int> InfoTicket = ticket.GetTicketInfo();

            Console.WriteLine("\n" + InfoTicket.Item1.Item3 + " " + InfoTicket.Item6.ToString("HH:mm dd/MM/yyyy"));

            string seats = "";

            for (int i = InfoTicket.Item2; i < InfoTicket.Item2 + InfoTicket.Item4; i++)
            {
                seats += "(" + i + "/" + InfoTicket.Item3 + ") ";
            }

            Console.WriteLine("Seats: " + seats);
            Console.WriteLine(InfoTicket.Item1.Item1 + " " + InfoTicket.Item1.Item2);
        }

        // Cancel the reservation and make the seats available again.
        /*private static void Cancel(int amount, int X, int Y, DateTimeHall DTH)
        {
            for (int i = X; i < amount + X; i++)
            {
                DTH.getDateInfo().Item4.getHallInfo().Item1[Y][i].editAvail();
            }
        }*/

        public static string selectTime(Tuple<List<DateTime>, List<int>, List<int>> date)
        {
            string CustomerReserve;
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the number or word that stands before the time you want to reserve or action you want to do");
                    CustomerReserve = Console.ReadLine();
                    if (CustomerReserve == "exit")
                    {
                        break;
                    }
                    else if (Convert.ToInt32(CustomerReserve) >= date.Item1.Count + 1 || Convert.ToInt32(CustomerReserve) < 1)
                    {
                        Console.WriteLine("Please enter a number that is an option");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter exit or a number that exists.");
                }
            }
            return CustomerReserve;
        }

        public static Tuple<Tuple<int, int, int>, List<Tuple<double, int, int, string, bool>>> hallSeatInfo(string CustomerReserve, Tuple<List<DateTime>, List<int>, List<int>> date)
        {
            AdminData AD = new AdminData();

            string datetime = date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("yyyy") + "-" + date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("MM") + "-" + date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("dd") + " " + date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("HH") + ":" + date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("mm");

            DateTime dt = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            int HallID = AD.GetHallID(AD.GetDateID(dt, date.Item3[Convert.ToInt32(CustomerReserve) - 1]));
            Tuple<int, int, int> HallInfo = AD.GetHallInfo(HallID);

            List<Tuple<double, int, int, string, bool>> seats = AD.GetSeat(HallID);

            return Tuple.Create(HallInfo, seats);
        }

        public static void reserveSeat(string whichMovie)
        {
            AdminData AD = new AdminData();
            Tuple<List<DateTime>, List<int>, List<int>> date = showTime(whichMovie);
            while (true)
            {
                string CustomerReserve = selectTime(date);

                if (CustomerReserve == "exit")
                {
                    break;
                }

                else
                {
                    int amount = 0;

                    Tuple<Tuple<int, int, int>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = hallSeatInfo(CustomerReserve, date);

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("\nPlease enter how many seats you want. (Maximum of 10 seats)");
                            amount = Convert.ToInt32(Console.ReadLine());
                            if (amount > 10 || amount < 1)
                            {
                                Console.WriteLine("\nPlease enter a number that is between 0 and 10.");
                            }
                            else
                            {
                                if (seatCheck(hallseatInfo.Item1, hallseatInfo.Item2, amount))
                                {
                                    showHall(hallseatInfo.Item1, hallseatInfo.Item2);
                                }
                                else
                                {
                                    Console.WriteLine("shame");
                                }
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nPlease enter a number.");
                        }
                    }
                }
            }
        }

        public static void showHall(Tuple<int, int, int> HallInfo, List<Tuple<double, int, int, string, bool>> seats)
        {
            string showhall = "";
            for (int i = 0; i < HallInfo.Item1; i++)
            {
                for (int j = 0; j < HallInfo.Item2; j++)
                {
                    for (int z = 0; z < seats.Count; z++)
                    {
                        if (seats[z].Item2 == i && seats[z].Item3 == j)
                        {
                            if (seats[z].Item5)
                            {
                                if (j > 8)
                                {
                                    showhall += " O ";
                                }
                                else if (j == 8)
                                {
                                    showhall += "O ";
                                }
                                else
                                {
                                    showhall += "O  ";
                                }
                            }
                            else if (seats[z].Item4 == "(No Seat)")
                            {
                                if (j > 8)
                                {
                                    showhall += "   ";
                                }
                                else if (j == 8)
                                {
                                    showhall += "  ";
                                }
                                else
                                {
                                    showhall += "   ";
                                }
                            }
                            else
                            {
                                if (j > 8)
                                {
                                    showhall += " X ";
                                }
                                else if (j == 8)
                                {
                                    showhall += "X ";
                                }
                                else
                                {
                                    showhall += "X  ";
                                }
                            }

                            break;
                        }
                    }
                }
                showhall += "\n";
            }

            Console.WriteLine(showhall);
        }

        public static bool seatCheck(Tuple<int, int, int> HallInfo, List<Tuple<double, int, int, string, bool>> seats, int amount)
        {
            AdminData AD = new AdminData();

            bool free = false;
            int count = 0;

            for (int i = 0; i < HallInfo.Item1; i++)
            {
                for (int j = 0; j < HallInfo.Item2; j++)
                {
                    for (int z = 0; z < seats.Count; z++)
                    {
                        if (seats[z].Item2 == i && seats[z].Item3 == j)
                        {
                            if (seats[z].Item5)
                            {
                                count++;
                                if (count >= amount)
                                {
                                    free = true;
                                    break;
                                }
                            }
                            else
                            {
                                count = 0;
                            }
                        }
                    }
                    if (free)
                    {
                        break;
                    }
                }
                if (free)
                {
                    break;
                }
                count = 0;
            }

            return free;
        }
        public static Tuple<List<DateTime>, List<int>, List<int>> showTime(string whichMovie)
        {
            AdminData AD = new AdminData();
            Tuple<List<DateTime>, List<int>, List<int>> times = AD.GetTime(Convert.ToInt32(whichMovie));

            for (int i = 0; i < times.Item1.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "]" + times.Item1[i].ToString("HH:mm dd/MM/yyyy"));
            }
            Console.WriteLine("[exit] Exit to movie menu");
            return times;
        }

        public static void Menu()
        {
            string whichMovie;
            string CustomerTimeOption;
            bool running = true;
            while (running)
            {
                // convert movielist count to a string
                Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");

                display();

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
                whichMovie = ShowMovieByInfo.ShowMovieByID(line);

                Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                while (true)
                {
                    CustomerTimeOption = Console.ReadLine();
                    // this will return the movie times for the movie you entered
                    //ShowMovieByInfo.ShowTimesByMovieID(whichMovie, CustomerTimeOption);
                    if (CustomerTimeOption == "1")
                    {
                        reserveSeat(whichMovie);
                        break;
                    }
                }
                Console.WriteLine("\nPlease enter the number or word that stands before the time you want to reserve or action you want to do");
                string CustomerReserve = Console.ReadLine();
            }     
        }
    }
}