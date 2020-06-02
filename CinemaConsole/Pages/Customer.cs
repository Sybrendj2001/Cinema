using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CinemaConsole.Data.BackEnd;


namespace CinemaConsole.Pages
{
    public class Customer
    {
        /// <summary>
        /// Shows a list of options
        /// </summary>
        /// <returns>A list of options</returns>
        public static List<int> Display()
        {
            Console.WriteLine("\nMovies:");

            ShowData showMovieInfo = new ShowData();
            List<int> IDList = showMovieInfo.ShowMovies();

            // check if user wants to go back
            Console.WriteLine("\n[menu] Restaurant Menu");
            Console.WriteLine("\n[exit] Back to the menu.");
            
            return IDList;
        }

        /// <summary>
        /// Menu to put in your name
        /// </summary>
        /// <returns>a tuple with full name and email</returns>
        private static Tuple<string, string, string> Name()
        {
            ShowData SD = new ShowData();
            Console.Clear();

            ProgressBalk(3);

            Console.WriteLine("\nPlease enter your first name");
            string first_name2 = Console.ReadLine();
            string first_name = first_name2.ToString().ToLower();

            Console.WriteLine("\nPlease enter your last name");
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
                    SD.ErrorMessage("\nPlease enter a valid e-mail adress");
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

        /// <summary>
        /// Select a time
        /// </summary>
        /// <param name="Date">Date to check</param>
        /// <param name="ID">ID of a time</param>
        /// <returns></returns>
        public static string selectTime(Tuple<List<DateTime>, List<int>, List<int>> Date, string ID)
        {
            string CustomerReserve;
            while (true)
            {
                ShowData SD = new ShowData();
                try
                {
                    Console.WriteLine("\nPlease enter the number or word that stands before the time you want to reserve or action you want to do");
                    CustomerReserve = Console.ReadLine();
                    if (CustomerReserve == "exit")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (CustomerReserve.Length > 5)
                    {
                        SD.ClearAndErrorMessage("Your input is too big");
                        showTime(ID);
                    }
                    else if (Convert.ToInt32(CustomerReserve) >= Date.Item1.Count + 1 || Convert.ToInt32(CustomerReserve) < 1)
                    {
                        SD.ErrorMessage("\nPlease enter an option that exists");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    SD.ErrorMessage("\nPlease enter an option that exists");
                }
            }
            return CustomerReserve;
        }

        /// <summary>
        /// Retracts all the info about a certain seat when you have the name and date
        /// </summary>
        /// <param name="CustomerReserve">ID of customer</param>
        /// <param name="Date">Date for the playdate</param>
        /// <returns>Info about the seat</returns>
        public static Tuple<Tuple<int, int, int, int, double, double, double>, List<Tuple<double, int, int, string, bool>>> hallSeatInfo(string CustomerReserve, Tuple<List<DateTime>, List<int>, List<int>> Date)
        {
            AdminData AD = new AdminData();

            string datetime = Date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("yyyy") + "-" + Date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("MM") + "-" + Date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("dd") + " " + Date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("HH") + ":" + Date.Item1[Convert.ToInt32(CustomerReserve) - 1].ToString("mm");

            DateTime dt = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            int HallID = AD.GetHallID(AD.GetDateID(dt, Date.Item3[Convert.ToInt32(CustomerReserve) - 1]));
            Tuple<int, int, int, int, double, double, double> HallInfo = AD.GetHallInfo(HallID);

            List<Tuple<double, int, int, string, bool>> seats = AD.GetSeat(HallID);

            return Tuple.Create(HallInfo, seats);
        }

        /// <summary>
        /// Reserve a seat
        /// </summary>
        /// <param name="WhichMovie">Movie</param>
        /// <returns>A tuple with the info</returns>
        public static Tuple<DateTime,int,int,int,int,Tuple<double,int,int>> reserveSeat(string WhichMovie)
        {
            AdminData AD = new AdminData();
            ShowData SD = new ShowData();
            DateTime datetime = DateTime.ParseExact("01/01/1900 01:01", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            int DateID = 0;
            int amount= 0;
            int seatX = 0;
            int seatY = 0;
            double price = 0.0;
            int hall = 0;
            int HallID = 0;
            ProgressBalk(1);
 
            while (true)
            {
                Tuple<List<DateTime>, List<int>, List<int>> date = showTime(WhichMovie);
                string CustomerReserve = selectTime(date, WhichMovie);

                if (CustomerReserve == "exit")
                {
                    Console.Clear();
                    break;
                }

                else
                {
                    amount = 0;

                    DateID = date.Item2[Convert.ToInt32(CustomerReserve)-1];
                    datetime = date.Item1[Convert.ToInt32(CustomerReserve) - 1];
                    hall = date.Item3[Convert.ToInt32(CustomerReserve) - 1];
                    Tuple<Tuple<int, int, int, int, double, double, double>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = hallSeatInfo(CustomerReserve, date);
                    HallID = hallseatInfo.Item1.Item4;
                    Console.Clear();
                    while (true)
                    {
                        try
                        {
                            ProgressBalk(2);

                            Console.WriteLine("\nPlease enter how many seats you want. (Maximum of 10 seats)");
                            string amount1 = Console.ReadLine();
                            if (amount1.Length > 5)
                            {
                                SD.ClearAndErrorMessage("Your input is too big");
                            }
                            else
                            {
                                amount = Convert.ToInt32(amount1);
                                if (amount > 10 || amount < 1)
                                {
                                    SD.ClearAndErrorMessage("\nPlease enter a number that is between 0 and 10.");                               
                                }
                                else
                                {
                                    if (seatCheck(hallseatInfo.Item1, hallseatInfo.Item2, amount))
                                    {
                                        Console.Clear();

                                        ProgressBalk(2);

                                        showHall(hallseatInfo.Item1, hallseatInfo.Item2);
                                        Tuple<int, int, double> chosenseats = chooseSeat(hallseatInfo.Item1, hallseatInfo.Item2, amount);
                                        seatX = chosenseats.Item1;
                                        seatY = chosenseats.Item2;
                                        price = chosenseats.Item3;
                                        break;
                                    }
                                    else
                                    {
                                        string seatsamount;
                                        while (true)
                                        {
                                            SD.ClearAndErrorMessage("\nThere are not enough seats left.\n[1] Choose another amount of seats\n[exit] Exit to movie list");
                                            seatsamount = Console.ReadLine();
                                            if (seatsamount == "1" || seatsamount == "exit")
                                            {
                                                break;
                                            }
                                        }
                                        if (seatsamount == "exit")
                                        {
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        catch (FormatException)
                        {
                            SD.ErrorMessage("\nPlease enter a number.");
                        }
                    }
                    break;
                }
            }
            return Tuple.Create(datetime,DateID, amount, seatX, seatY, Tuple.Create(price, hall, HallID));
        }

        /// <summary>
        /// Function to show the hall
        /// </summary>
        /// <param name="HallInfo">All the info about the hall itself</param>
        /// <param name="Seats">All the info of the seats</param>
        public static void showHall(Tuple<int, int, int, int, double, double, double> HallInfo, List<Tuple<double, int, int, string, bool>> Seats)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\nLegend:");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("O");
            Console.ResetColor();
            Console.Write(" - €" + HallInfo.Item5.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\n");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("O");
            Console.ResetColor();
            Console.Write(" - €" + HallInfo.Item6.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\n");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");
            Console.ResetColor();
            Console.Write(" - €" + HallInfo.Item7.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "\n");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ResetColor();
            Console.Write(" - Reserved Seat\n");

            string XasNumbers = "\n";
            
            for (int i = 0; i < HallInfo.Item2; i++)
            {
                if (i > 8)
                {
                    XasNumbers += (i+1) +" ";
                }
                else if (i == 8)
                {
                    XasNumbers += (i+1) + "  ";
                }
                else
                {
                    XasNumbers += (i+1) + "  ";
                }
            }
            XasNumbers += "\n";
            Console.WriteLine(XasNumbers);
            
            for (int i = 0; i < HallInfo.Item1; i++)
            {
                for (int j = 0; j < HallInfo.Item2; j++)
                {
                    for (int z = 0; z < Seats.Count; z++)
                    {
                        if (Seats[z].Item2 == i && Seats[z].Item3 == j)
                        {
                            if (Seats[z].Item5)
                            {
                                if (Seats[z].Item1 == HallInfo.Item5)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                else if (Seats[z].Item1 == HallInfo.Item6)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                }
                                else if(Seats[z].Item1 == HallInfo.Item7)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }

                                if (j > 8)
                                {
                                    Console.Write(" O ");
                                }
                                else if (j == 8)
                                {
                                    Console.Write("O ");
                                }
                                else
                                {
                                    Console.Write("O  ");
                                }
                                Console.ResetColor();
                            }
                            else if (Seats[z].Item4 == "(No Seat)")
                            {
                                if (j > 8)
                                {
                                    Console.Write("   ");
                                }
                                else if (j == 8)
                                {
                                    Console.Write("  ");
                                }
                                else
                                {
                                    Console.Write("   ");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (j > 8)
                                {
                                    Console.Write(" X ");
                                }
                                else if (j == 8)
                                {
                                    Console.Write("X ");
                                }
                                else
                                {
                                    Console.Write("X  ");
                                }
                                Console.ResetColor();
                            }
                            break;
                        }
                    }
                }
                Console.Write("  " + (HallInfo.Item1 - i));
                Console.Write("\n");
            }

            string screen = "";
            screen += "\n";
            for (int i = 0; i < HallInfo.Item2; i++)
            {
                screen += "---";
            }

            screen += "       (screen)\n";
            Console.WriteLine(screen);
        }

        /// <summary>
        /// Check if you can reserve the seats
        /// </summary>
        /// <param name="HallInfo">Which hall is selected</param>
        /// <param name="Seats">Where the seat start</param>
        /// <param name="Amount">The amount of the seats you want to check</param>
        /// <returns></returns>
        public static bool seatCheck(Tuple<int, int, int, int, double, double, double> HallInfo, List<Tuple<double, int, int, string, bool>> Seats, int Amount)
        {
            AdminData AD = new AdminData();

            bool free = false;
            int count = 0;

            for (int i = 0; i < HallInfo.Item1; i++)
            {
                for (int j = 0; j < HallInfo.Item2; j++)
                {
                    for (int z = 0; z < Seats.Count; z++)
                    {
                        if (Seats[z].Item2 == i && Seats[z].Item3 == j)
                        {
                            if (Seats[z].Item5)
                            {
                                count++;
                                if (count >= Amount)
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
        
        /// <summary>
        /// Shows all the times of a movie
        /// </summary>
        /// <param name="WhichMovie">Movie to check all the times for</param>
        /// <returns>A list with all the times for that movie</returns>
        public static Tuple<List<DateTime>, List<int>, List<int>> showTime(string WhichMovie)
        {
            AdminData AD = new AdminData();
            Tuple<List<DateTime>, List<int>, List<int>> times = AD.GetTime(Convert.ToInt32(WhichMovie));

            for (int i = 0; i < times.Item1.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + times.Item1[i].ToString("HH:mm dd/MM/yyyy"));
            }
            Console.WriteLine("[exit] Exit to menu");
            return times;
        }

        /// <summary>
        /// Choose a seat in a certain hall
        /// </summary>
        /// <param name="HallInfo">Info about the hall</param>
        /// <param name="Seats">Where the reservation starts</param>
        /// <param name="Amount">The amount of seats you want to reserve</param>
        /// <returns>The start place and price</returns>
        public static Tuple<int, int, double> chooseSeat(Tuple<int, int, int, int, double, double, double> HallInfo, List<Tuple<double, int, int, string, bool>> Seats, int Amount)
        {
            ShowData SD = new ShowData();
            AdminData AD = new AdminData();
            int seatX = 0;
            int seatY = 0;
            double price = 0.0;

            bool free = true;

            while (true)
            {
                Console.WriteLine("Please enter the most left seat you want to reserve like this x/y or type [exit] to leave the reservation. (e.g. 5/3)");
                string selected = Console.ReadLine();
                
                free = true;
                bool exist1 = false;
                bool exist2 = false;

                if (selected == "exit")
                {
                    Console.Clear();
                    free = false;
                    break;
                }
                else if (selected.Length > 7)
                {
                    SD.ErrorMessage("Your input is too big");
                }
                else
                {


                    string[] selectedSeat = selected.Split('/');

                    try
                    {

                        seatX = Convert.ToInt32(selectedSeat[0]);
                        seatY = HallInfo.Item1 - Convert.ToInt32(selectedSeat[1]) + 1;


                        for (int i = 0; i < Seats.Count; i++)
                        {
                            if ((seatY - 1 == Seats[i].Item2) && ((Seats[i].Item3 >= seatX - 1) && (Seats[i].Item3 < seatX - 1 + Amount)) && !Seats[i].Item5)
                            {
                                free = false;
                                price = 0.0;
                                //break;
                            }
                            if ((seatY - 1 == Seats[i].Item2) && ((Seats[i].Item3 >= seatX - 1) && (Seats[i].Item3 < seatX - 1 + Amount)) && Seats[i].Item5)
                            {
                                price += Seats[i].Item1;
                            }
                            if (seatY - 1 == Seats[i].Item2 && Seats[i].Item3 == seatX - 1 && Seats[i].Item4 != "(No Seat)")
                            {
                                exist1 = true;
                            }
                            if (seatY - 1 == Seats[i].Item2 && Seats[i].Item3 == seatX + Amount - 2 && Seats[i].Item4 != "(No Seat)")
                            {
                                exist2 = true;
                            }
                        }

                        if ((exist1 && exist2) || !free)
                        {
                            if (free)
                            {
                                break;
                            }
                            else if (!exist1 || !exist2)
                            {
                                Console.WriteLine("\nMake sure your seats are in the theatherhall");
                            }
                            else
                            {
                                SD.ErrorMessage("\nThere are not enough seats free from this point.");
                            }
                        }
                        else
                        {
                            SD.ErrorMessage("\nMake sure your seats are in the theatherhall");
                        }

                    }
                    catch (FormatException)
                    {
                        SD.ErrorMessage("\nPlease enter it like in the example.");
                    }
                    //Catches if the user put in no / and if it is not out of bounce the theaterhall
                    catch (IndexOutOfRangeException)
                    {
                        SD.ErrorMessage("\nMake sure your seats are in the theatherhall and it is written like in the example.");
                    }
                }
            }

            if (free)
            {
                AD.switchAvail((seatX - 1), (seatY - 1), HallInfo.Item4, Amount, false);
            }

            return Tuple.Create(seatX, seatY,price);
        }

        /// <summary>
        /// Customer get an overview of all the information about the movie and contact details before booking
        /// </summary>
        /// <param name="PersonInfo">Information about the person</param>
        /// <param name="TicketInfo">Information about the ticket that they are about to buy</param>
        /// <param name="Title">Title of the movie</param>
        /// <param name="TicketCode">Code of the ticket</param>
        public static void overviewCustomer(Tuple<string, string, string> PersonInfo, Tuple<DateTime, int, int, int, int, Tuple<double, int, int>> TicketInfo, string Title, string TicketCode)
        {
            string totalprice = TicketInfo.Item6.Item1.ToString("0.00");
            string datetime = Convert.ToDateTime(TicketInfo.Item1).ToString("dd/MM/yyyy HH:mm");

            ProgressBalk(4);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nMovie: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Title);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nTime: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(datetime);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nTotal price: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("€" + totalprice);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            int Y = 0;

            if (TicketInfo.Item6.Item2 == 1)
            {
                Y = 14 - TicketInfo.Item5 + 1;
            }
            else if (TicketInfo.Item6.Item2 == 2)
            {
                Y = 19 - TicketInfo.Item5 + 1;
            }
            else if (TicketInfo.Item6.Item2 == 3)
            {
                Y = 20 - TicketInfo.Item5 + 1;
            }

            Console.Write("\nSeats:");
            string seats = "";
            for (int i = TicketInfo.Item4; i < TicketInfo.Item4 + TicketInfo.Item3; i++)
            {
                seats += " (" + i + "/" + Y + ")";
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(seats);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nName: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(PersonInfo.Item1 + " " + PersonInfo.Item2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\nEmail: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(PersonInfo.Item3 + "\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Creates a ticketID for the customer
        /// </summary>
        /// <param name="Time">The time the movie starts</param>
        /// <param name="MovieName">The name of the movie</param>
        /// <param name="X">The X start position the reservation</param>
        /// <param name="Y">The Y start position the reservation</param>
        /// <param name="TheatherHall">The hall the movie is playing</param>
        /// <returns>The ticketID</returns>
        private static string createTicketID(DateTime Time, string MovieName, int X, int Y, int TheatherHall)
        {
            //Takes the first 3 letters of the movie and makes them all caps
            string MovieNameShort = MovieName.Substring(0, 3).ToUpper();

            //Create the movie unique id
            string MovieTicketData = (Time.ToString("mm")) + (Time.ToString("HH")) + (Time.ToString("dd")) +
                (Time.ToString("MM")) + (Time.ToString("yyyy")) + MovieNameShort + X + Y + TheatherHall;

            return MovieTicketData;
        }

        /// <summary>
        /// Switching between menus
        /// </summary>
        /// <param name="place">Place the menu is currently at</param>
        private static void ProgressBalk(int place)
        {
            if(place == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Choose time");
                Console.ResetColor();
                Console.Write(" - ");
                Console.Write("Choose seat(s)");
                Console.Write(" - ");
                Console.Write("Personal information");
                Console.Write(" - ");
                Console.Write("Overview");
                Console.Write(" - ");
                Console.Write("Ticket\n\n");
            }
            else if(place == 2)
            {
                Console.Write("Choose time");
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Choose seat(s)");
                Console.ResetColor();
                Console.Write(" - ");
                Console.Write("Personal information");
                Console.Write(" - ");
                Console.Write("Overview");
                Console.Write(" - ");
                Console.Write("Ticket\n\n");
            }
            else if (place == 3)
            {
                Console.Write("\nChoose time");
                Console.Write(" - ");
                Console.Write("Choose seat(s)");
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Personal information");
                Console.ResetColor();
                Console.Write(" - ");
                Console.Write("Overview");
                Console.Write(" - ");
                Console.Write("Ticket\n");
            }
            else if (place == 4)
            {
                Console.Write("\nChoose time");
                Console.Write(" - ");
                Console.Write("Choose seat(s)");
                Console.Write(" - ");
                Console.Write("Personal information");
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Overview");
                Console.ResetColor();
                Console.Write(" - ");
                Console.Write("Ticket\n");
            }
            else if (place == 5)
            {
                Console.Write("\nChoose time");
                Console.Write(" - ");
                Console.Write("Choose seat(s)");
                Console.Write(" - ");
                Console.Write("Personal information");
                Console.Write(" - ");
                Console.Write("Overview");
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Ticket\n");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Menu for the customer
        /// </summary>
        public static void Menu()
        {
            Console.Clear();
            ShowData SD = new ShowData();
            ChangeData CD = new ChangeData();
            AdminData AD = new AdminData();
            string whichMovie;
            string title;
            string CustomerAge;
            string movieAgeQualification;
            string CustomerTimeOption;
            bool running = true;
            while (running)
            {
                // convert movielist count to a string
                Console.WriteLine("\nPlease enter the number or word that stands before the movie you want to reserve or action you want to do.");

                List<int> MovieIDs = Display();

                string line = Console.ReadLine();
                try
                {

                    // check if user wants to go back 
                    if (line == "exit")
                    {
                        break;
                    }
                    else if (line.Length > 5)
                    {
                        SD.ErrorMessage("Your input is too big");
                    }
                    else if (line == "menu")
                    {
                        CD.DisplayProducts();
                        Console.WriteLine("\nPress enter to get back to the movielist");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else if (MovieIDs.Contains(Convert.ToInt32(line)))
                    {
                        // this will return the movie details for the number you entered
                        Tuple<string, string, string> showmovieinfo = SD.ShowMovieByID(line);
                        title = showmovieinfo.Item2;
                        whichMovie = showmovieinfo.Item1;
                        movieAgeQualification = showmovieinfo.Item3;

                        while (true)
                        {
                            Console.WriteLine("\nAre you over " + movieAgeQualification + " years old?");
                            Console.WriteLine("[1] Yes\n[2] No");
                            CustomerAge = Console.ReadLine();
                            if (CustomerAge.Length > 5)
                            {
                                SD.ClearAndErrorMessage("Your input is too big");
                            }
                            else if (CustomerAge == "1")
                            {
                                Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                                CustomerTimeOption = Console.ReadLine();
                                // this will return the movie times for the movie you entered
                                if (CustomerTimeOption.Length > 5)
                                {
                                    SD.ClearAndErrorMessage("Your input is too big");
                                }
                                else if (CustomerTimeOption == "1")
                                {
                                    Tuple<DateTime, int, int, int, int, Tuple<double, int, int>> ticket = reserveSeat(whichMovie);

                                    if (ticket.Item5 == 0.0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Tuple<string, string, string> personInfo = Name();
                                        string ticketcode = createTicketID(ticket.Item1, title, ticket.Item4, ticket.Item5, ticket.Item6.Item2);
                                        string confirm;
                                        Console.Clear();
                                        while (true)
                                        {
                                            overviewCustomer(personInfo, ticket, title, ticketcode);
                                            Console.WriteLine("\nDo you want to confirm the reservation? \n[1] Confirm reservation\n[2] Cancel reservation");
                                            confirm = Console.ReadLine();
                                            if (confirm.Length > 5)
                                            {
                                                SD.ClearAndErrorMessage("Your input is too big");
                                            }
                                            else if (confirm == "1")
                                            {
                                                Console.Clear();

                                                ProgressBalk(5);

                                                CD.ReserveTicket((personInfo.Item1 + " " + personInfo.Item2), personInfo.Item3, ticketcode, Convert.ToInt32(whichMovie), ticket.Item3, ticket.Item4, ticket.Item5, ticket.Item2, ticket.Item6.Item2, ticket.Item6.Item1, ticket.Item6.Item3);
                                                Console.WriteLine("\nReservation completed\nPlease write this down or remember it well.\nTicket: " + ticketcode);
                                                Console.WriteLine("\nPress enter to continue");
                                                Console.ReadLine();
                                                Admin.Admin.UpdateRevenue(ticketcode);
                                                Console.Clear();
                                                break;
                                            }
                                            else if (confirm == "2")
                                            {
                                                //Cancel the seats
                                                AD.switchAvail((ticket.Item4 - 1), (ticket.Item5 - 1), ticket.Item6.Item3, ticket.Item3, true);
                                                Console.Clear();
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else if (CustomerTimeOption == "exit")
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    SD.ErrorMessage("\nPlease enter an option that exists");
                                }
                            }
                            else if (CustomerAge == "2")
                            {
                                // If you're under 12 you can take someone who is 18 years or older with you
                                if (int.Parse(movieAgeQualification) <= 12)
                                {
                                    Console.WriteLine("\nYou're not old enough for this movie \nYou can only go if you take someone who is 18 years or older with you\nPlease make sure the person of 18 years or older reserves the tickets\n\nPress enter to continue");
                                }
                                else
                                {
                                    Console.WriteLine("\nYou're not old enough for this movie\nPress enter to continue");
                                }
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                SD.ClearAndErrorMessage("\nPlease enter an option that exists");
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    SD.ClearAndErrorMessage("\nPlease enter an option that exists");
                }
            }     
        }
    }
}