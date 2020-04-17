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
        /// <summary>
        /// let's you choose the place of your seat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <param name="amount"></param>
        public static Tuple<int, int> ChooseSeat(int id, Movies movie, int amount)
        {
            int seatX = 0;
            int seatY = 0;

            foreach (DateTimeHall time in movie.DateTimeHallsList)
            {
                if (time.getDateInfo().Item1 == id)
                {
                    //This is the hall of the movie
                    Seat[][] seat = time.getDateInfo().Item4.getHallInfo().Item1;

                    bool k = true;
                    bool free = true;
                    //This loop will let you choose 
                    while (k)
                    {
                        free = true;
                        Console.WriteLine("Please enter the most left seat you want to reserve like this x/y or type [exit] to leave the reservation. (5/3)");
                        string selected = Console.ReadLine();
                        if (selected == "exit")
                        {
                            //skip the last if statement
                            free = false;
                    
                            break;
                        }
                        string[] selectedSeat = selected.Split('/');

                        //changes the string number to intergers and checks if the seats chosen are free
                        try
                        {
                            seatX = Convert.ToInt32(selectedSeat[0]);
                            seatY = Convert.ToInt32(selectedSeat[1]);

                            for (int i = seatX - 1; i < (seatX + amount - 1); i++)
                            {
                                if (!seat[seatY - 1][i].getInfo().Item3)
                                {
                                    free = false;
                                }
                            }
                            if (free)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nThere are not enough seats free from this point.");
                            }

                        }
                        //Catches if the user put in a number
                        catch (FormatException)
                        {
                            Console.WriteLine("\nPlease enter it like in the example.");
                        }
                        //Catches if the user put in no / and if it is not out of bounce the theaterhall
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("\nMake sure your seats are in the theatherhall and it is written like in the example.");
                        }
                    }

                    //edits the seat to opposite of what it was
                    if (free)
                    {
                        Cancel(amount, (seatX-1), (seat.Length - seatY), time);
                    }
                    break;
                }
            }
            return Tuple.Create(seatX, seatY);
        }

        public static string GetMovieInfo(Movies movie)
        {
            Console.WriteLine("\nMovie selected: " + movie.getMovieInfo().Item2);
            Console.WriteLine("Year: " + movie.getMovieInfo().Item3);
            Console.WriteLine("Age restriction: " + movie.getMovieInfo().Item4 + "+");
            Console.WriteLine("Actors: " + movie.getMovieInfo().Item6);
            Console.WriteLine("Summary: " + movie.getMovieInfo().Item5);
            string CustomerReservateOption = "";
            
            while (true)
            {
                Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                CustomerReservateOption = Console.ReadLine();
                Console.WriteLine("");

                if (CustomerReservateOption == "1")
                {
                    foreach (DateTimeHall date in movie.DateTimeHallsList)
                    {
                        Console.WriteLine("[" + date.getDateInfo().Item1 + "] " + date.getDateInfo().Item3 + "      " + date.getDateInfo().Item2);
                    }

                    Console.WriteLine("[exit] Back to menu");
                    break;
                }

                else if (CustomerReservateOption == "exit")
                {
                    break;
                }
            }
            return CustomerReservateOption;
        }

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
        private static void Cancel(int amount, int X, int Y, DateTimeHall DTH)
        {
            for (int i = X; i < amount + X; i++)
            {
                DTH.getDateInfo().Item4.getHallInfo().Item1[Y][i].editAvail();
            }
        }


        public static void reserveSeat(string whichMovie)
        {
            AdminData AD = new AdminData();
            Tuple<List<DateTime>, List<int>, List<int>> date = showTime(whichMovie);
            while (true)
            {
                try
                {
                    Console.WriteLine("\nPlease enter the number or word that stands before the time you want to reserve or action you want to do");
                    string CustomerReserve = Console.ReadLine();
                    if (CustomerReserve == "exit")
                    {
                        break;
                    }
                    else if (Convert.ToInt32(CustomerReserve) > date.Item1.Count || Convert.ToInt32(CustomerReserve) < 1)
                    {
                        Console.WriteLine("Please enter a number that is an option");
                    }
                    else
                    {
                        int amount = 0;

                        string datetime = date.Item1[Convert.ToInt32(CustomerReserve)-1].ToString("yyyy") + "-" + date.Item1[Convert.ToInt32(CustomerReserve)-1].ToString("MM") + "-" + date.Item1[Convert.ToInt32(CustomerReserve)-1].ToString("dd") + " " + date.Item1[Convert.ToInt32(CustomerReserve)-1].ToString("HH") + ":" + date.Item1[Convert.ToInt32(CustomerReserve)-1].ToString("mm");

                        DateTime dt = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                        int HallID = AD.GetHallID(AD.GetDateID(dt, date.Item3[Convert.ToInt32(CustomerReserve)-1]));
                        Tuple<int, int, int> HallInfo = AD.GetHallInfo(HallID);

                        List<Tuple<double, int, int, string, bool>> seats = AD.GetSeat(HallID);

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
                                    if (seatCheck(HallInfo, seats, amount))
                                    {
                                        showHall(HallInfo, seats);
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
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter exit or a number that exists.");
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