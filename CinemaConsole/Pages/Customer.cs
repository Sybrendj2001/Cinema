using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Adding some data, so you won't have to create new movies everytime you run the application
        /// </summary>
        public static void AddStuff()
        {
            Movies movie1 = new Movies("Transformers", 2007, 12, "An ancient struggle between two Cybertronian races, the heroic Autobots and the evil Decepticons, comes to Earth, with a clue to the ultimate power held by a teenager.", "Shia LaBeouf, Megan Fox");
            MovieList.movieList.Add(movie1);

            DateTimeHall datetimehall1 = new DateTimeHall("04/04/2020", "09:00", 1, movie1);
            movie1.DateTimeHallsList.Add(datetimehall1);
            DateTimeHall datetimehall1A = new DateTimeHall("21/06/2020", "12:20", 2, movie1);
            movie1.DateTimeHallsList.Add(datetimehall1A);

            Movies movie2 = new Movies("Avengers", 2012, 12, "Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.", "Robert Downey Jr., Chris Evans, Scarlett Johansson");
            MovieList.movieList.Add(movie2);

            DateTimeHall datetimehall2 = new DateTimeHall("21/05/2020", "12:20", 1, movie2);
            movie2.DateTimeHallsList.Add(datetimehall2);
            DateTimeHall datetimehall2A = new DateTimeHall("21/06/2020", "12:20", 3, movie2);
            movie2.DateTimeHallsList.Add(datetimehall2A);

            Movies movie3 = new Movies("The Dark Knight", 2008, 12, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", " Christian Bale, Heath Ledger, Aaron Eckhart");
            MovieList.movieList.Add(movie3);

            DateTimeHall datetimehall3 = new DateTimeHall("21/06/2020", "12:20", 1, movie3);
            movie3.DateTimeHallsList.Add(datetimehall3);
            DateTimeHall datetimehall3A = new DateTimeHall("21/05/2020", "12:20", 2, movie3);
            movie3.DateTimeHallsList.Add(datetimehall3A);
        }

        /// <summary>
        /// seatCheck returns if there is enough room for the chosen number of seats
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static bool SeatCheck(int id, Movies movie, int amount)
        {
            bool check = false;

            foreach (DateTimeHall time in movie.DateTimeHallsList)
            {
                if (time.getDateInfo().Item1 == id)
                {
                    //This is the hall of the movie
                    Seat[][] seat = time.getDateInfo().Item4.getHallInfo().Item1;

                    //this checks if there is enough room for the amount of seats
                    //If not check remains false else it becomes true
                    for (int i = 0; i < seat.Length; i++)
                    {
                        int count = 0;
                        for (int j = 0; j < seat[i].Length; j++)
                        {
                            if (seat[seat.Length - 1 - i][j].getInfo().Item3)
                            {
                                count++;
                                if (count >= amount)
                                {
                                    check = true;
                                }
                            }
                            else
                            {
                                count = 0;
                            }
                        }
                    }
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// Is a function to show the theaterhall
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        private static void ShowHall(int id, Movies movie)
        {
            foreach (DateTimeHall time in movie.DateTimeHallsList)
            {
                if (time.getDateInfo().Item1 == id)
                {
                    //This is the hall of the movie
                    Seat[][] seat = time.getDateInfo().Item4.getHallInfo().Item1;
                    string show = "\n";

                    //This for loop gives numbers ontop of the raster
                    //The difference in spaces is because of the extra number if collum becomes bigger than 9(i > 8) 
                    for (int i = 0; i < seat[0].Length; i++)
                    {
                        if (i > 8)
                        {
                            show += (i + 1) + " ";
                        }
                        else if (i == 8)
                        {
                            show += (i + 1) + " ";
                        }
                        else
                        {
                            show += (i + 1) + "  ";
                        }
                    }
                    show += "\n";
                    //If avail of seat(getInfo.Item3) is true then place a O else place a X
                    for (int i = 0; i < seat.Length; i++)
                    {
                        for (int j = 0; j < seat[i].Length; j++)
                        {
                            if (seat[i][j].getInfo().Item3)
                            {
                                if (j > 8)
                                {
                                    show += " O ";
                                }
                                else if (j == 8)
                                {
                                    show += "O ";
                                }
                                else
                                {
                                    show += "O  ";
                                }
                            }

                            else if (seat[i][j].getInfo().Item2 == "(No Seat)")
                            {
                                if (j > 8)
                                {
                                    show += "   ";
                                }
                                else if (j == 8)
                                {
                                    show += "  ";
                                }
                                else
                                {
                                    show += "   ";
                                }
                            }
                            else
                            {
                                if (j > 8)
                                {
                                    show += " X ";
                                }
                                else if (j == 8)
                                {
                                    show += "X ";
                                }
                                else
                                {
                                    show += "X  ";
                                }
                            }
                        }
                        show += (seat.Length - i) + "\n";
                    }

                    show += "\n";
                    for (int i = 0; i < seat[0].Length; i++)
                    {
                        show += "---";
                    }

                    show += "       (screen)\n";
                    Console.WriteLine(show);
                }
            }
        }

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
                                Console.WriteLine("There are not enough seats free from this point.");
                            }

                        }
                        //Catches if the user put in a number
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter it like in the example.");
                        }
                        //Catches if the user put in no / and if it is not out of bounce the theaterhall
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Make sure your seats are in the theatherhall and it is written like in the example.");
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

        /// <summary>
        /// This starts the process of selecting the amount of seats and the place of the seats
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="id"></param>
        public static Tuple<int, int, int> SelectSeat(Movies movie, int id)
        {
            int amount = 0;
            bool k = true;
            while (k)
            {
                //gets a number and checks if it is higher than 0 and smaller than 11
                try
                {
                    Console.WriteLine("Please enter how many seats you want. (Maximum of 10 seats)");
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount > 10 || amount < 1)
                    {
                        Console.WriteLine("Please enter a number that is between 0 and 10.");
                    }
                    else
                    {
                        bool seatCheck = SeatCheck(id, movie, amount);

                        if (seatCheck)
                        {
                            ShowHall(id, movie);
                            Tuple<int, int> XY = ChooseSeat(id, movie, amount);
                            int X = XY.Item1;
                            int Y = XY.Item2;
                            return Tuple.Create(amount, X, Y);
                        }
                        else
                        {
                            Console.WriteLine("There are not enough seats left. Type [1] if you want to reserve different amount of seats. Else type [2]");
                            string again = Console.ReadLine();
                            if (again == "2")
                            {
                                return Tuple.Create(0, 0, 0);
                            }
                        }
                    }
                }
                //Catches if the user did not use a number
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number.");
                }
            }
            return Tuple.Create(0, 0, 0);

        }

        public static string GetMovieInfo(Movies movie)
        {
            Console.WriteLine("Movie selected: " + movie.getMovieInfo().Item2);
            Console.WriteLine("Year: " + movie.getMovieInfo().Item3);
            Console.WriteLine("Age restriction: " + movie.getMovieInfo().Item4 + "+");
            Console.WriteLine("Actors: " + movie.getMovieInfo().Item6);
            Console.WriteLine("Summary: " + movie.getMovieInfo().Item5);
            string CustomerReservateOption = "";
            while (true)
            {
                Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                CustomerReservateOption = Console.ReadLine();

                if (CustomerReservateOption == "1")
                {
                    foreach (DateTimeHall date in movie.DateTimeHallsList)
                    {
                        Console.WriteLine("[" + date.getDateInfo().Item1 + "] " + date.getDateInfo().Item2 + "      " + date.getDateInfo().Item3);
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
            Console.WriteLine("Movies:");

            // Loop trough all movies currently in the movielist
            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
            }

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

            Console.WriteLine("Please enter your e-mail adress");
            string email = Console.ReadLine();

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

        public static void Menu()
        {
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

                foreach (Movies aMovie in MovieList.movieList)
                {
                    if (line == aMovie.getMovieInfo().Item1.ToString())
                    {
                        string Movieinfo = GetMovieInfo(aMovie);

                        if (Movieinfo == "1")
                        {
                            while (true)
                            {
                                Console.WriteLine("\nPlease enter the number or word that stands before the time you want to reserve or action you want to do");
                                string CustomerReserve = Console.ReadLine();

                                if (CustomerReserve == "exit")
                                {
                                    break;
                                }

                                else
                                {
                                    int Break = 0;
                                    foreach (DateTimeHall date in aMovie.DateTimeHallsList)
                                    {
                                        if (CustomerReserve == date.getDateInfo().Item1.ToString())
                                        {
                                            Tuple<int, int, int> AmountXY = SelectSeat(aMovie, date.getDateInfo().Item1);
                                            Break = AmountXY.Item1;

                                            if (AmountXY.Item1 == 0)
                                            {
                                                break;
                                            }

                                            else
                                            {
                                                Tuple<string, string, string> Information = Name();
                                                string fullname = Information.Item1 + " " + Information.Item2;

                                                string Date = date.getDateInfo().Item2 + " " + date.getDateInfo().Item3;
                                                int Theatherhall = date.getDateInfo().Item4.getHallInfo().Item2;
                                                TicketInfo ticket = new TicketInfo(fullname, Information.Item3, AmountXY.Item2, AmountXY.Item3, AmountXY.Item1, 15.00, Date, aMovie.getMovieInfo().Item2, Theatherhall);

                                                Overview(ticket);
                                                Console.WriteLine("\nDo you want to confirm the reservation? \n[1] Confirm reservation\n[2] Cancel reservation");
                                                string confirm = Console.ReadLine();

                                                while (true)
                                                {
                                                    //Conform the reservation or cancel it.
                                                    if (confirm == "1")
                                                    {
                                                        ReservationList.reservationList.Add(ticket);
                                                        Console.WriteLine("\nReservation completed\nPlease write this down or remember it well.\nTicket: " + ticket.GetTicketInfo().Item1.Item4);
                                                        Console.WriteLine("\nEnter to go back to the movielist");
                                                        Console.ReadLine();
                                                        break;
                                                    }

                                                    else if (confirm == "2")
                                                    {
                                                        Cancel(AmountXY.Item1, AmountXY.Item2, AmountXY.Item3, date);
                                                        break;
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }

                                    if (Break != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}