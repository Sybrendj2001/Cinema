using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Pages.Customer;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data;
using CinemaConsole.Data.BackEnd;
using System.Globalization;

namespace CinemaConsole.Pages.Admin
{
    public class Admin : Employee
    {
        public Admin()
        {
        }

        private static void ShowHallPriceDistribution(int hall)
        {
            if (hall == 1)
            {
                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        //Gives the right colors
                        if ((j == 5 || j == 6) && (i > 4 && i < 9))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j == 5 || j == 6) && (i > 2 && i < 11))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 4 || j == 7) && (i > 3 && i < 10))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 3 || j == 8) && (i > 4 && i < 9))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        //Makes the hall
                        if ((i == 0 || i > 11) && (j > 1 && j < 10))
                        {
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
                        }
                        else if (i > 2 && i < 11)
                        {
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
                        }
                        else if ((i == 1 || i == 2 || i == 11) && (j > 0 && j < 11))
                        {
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
                        }
                        else
                        {
                            if (j == 8)
                            {
                                Console.Write("  ");
                            }
                            else
                            {
                                Console.Write("   ");
                            }
                        }
                        Console.ResetColor();
                    }
                    Console.Write("\n");
                }
            }
            else if (hall == 2)
            {
                for (int i = 0; i < 19; i++)
                {
                    for (int j = 0; j < 18; j++)
                    {
                        //Gives the right colors
                        if ((j == 8 || j == 9) && (i > 4 && i < 13))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j == 7 || j == 10) && (i > 5 && i < 12))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j == 6 || j == 11) && (i > 6 && i < 11))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j > 5 && j < 12) && (i > 0 && i < 16))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 5 || j == 12) && (i > 1 && i < 14))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 4 || j == 13) && (i > 3 && i < 13))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 3 || j == 14) && (i > 5 && i < 12))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 2 || j == 15) && (i > 7 && i < 11))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        //Makes the hall
                        if ((i == 18 || i == 17) && (j > 2 && j < 15))
                        {
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
                        }
                        else if ((i < 17 && i > 13) && (j > 1 && j < 16))
                        {
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
                        }
                        else if ((i < 6 || (i > 10 && i < 14)) && (j > 0 && j < 17))
                        {
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
                        }
                        else if (i > 5 && i < 11)
                        {
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
                        }
                        else
                        {
                            if (j == 8)
                            {
                                Console.Write("  ");
                            }
                            else
                            {
                                Console.Write("   ");
                            }
                        }
                        Console.ResetColor();
                    }
                    Console.Write("\n");
                }
            }
            else if (hall == 3)
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        //Gives the right colors
                        if ((j > 12 && j < 17) && (i > 3 && i < 13))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j == 12 || j == 17) && (i > 4 && i < 12))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j == 11 || j == 18) && (i > 5 && i < 12))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if ((j > 11 && j < 18) && (i > 0 && i < 17))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 10 || j == 11 || j == 18 || j == 19) && (i > 0 && i < 16))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 9 || j == 20) && (i > 0 && i < 15))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 8 || j == 21) && (i > 1 && i < 14))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 7 || j == 22) && (i > 3 && i < 12))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 6 || j == 23) && (i > 5 && i < 11))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((j == 5 || j == 24) && (i > 7 && i < 10))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        //Makes the hall
                        if (i == 19 && (j > 7 && j < 22))
                        {
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
                        }
                        else if (i == 18 && (j > 6 && j < 23))
                        {
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
                        }
                        else if (i == 17 && (j > 4 && j < 25))
                        {
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
                        }
                        else if (i == 0 && (j > 3 && j < 26))
                        {
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
                        }
                        else if ((i == 16 || i == 15 || (i < 5 && i > 0)) && (j > 2 && j < 27))
                        {
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
                        }
                        else if ((i == 14 || i == 13 || i == 5) && (j > 1 && j < 28))
                        {
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
                        }
                        else if ((i == 12 || i == 6) && (j > 0 && j < 29))
                        {
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
                        }
                        else if (i > 6 && i < 12)
                        {
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
                        }
                        else
                        {
                            if (j == 8)
                            {
                                Console.Write("  ");
                            }
                            else
                            {
                                Console.Write("   ");
                            }
                        }
                        Console.ResetColor();
                    }
                    Console.Write("\n");
                }
            }
        }

        private static void editPrice()
        {
            AdminData AD = new AdminData();
            while (true)
            {
                Console.WriteLine("\nIn what hall would like to edit a price: [1]  [2]  [3]\n[exit] Exit to menu");
                string choice = Console.ReadLine();

                try
                {
                    if (choice == "exit")
                    {
                        break;
                    }
                    else if (Convert.ToInt32(choice) > 0 && Convert.ToInt32(choice) < 4)
                    {
                        ShowHallPriceDistribution(Convert.ToInt32(choice));
                        Tuple<double, double, double> prices = AD.getPrices(Convert.ToInt32(choice));

                        Console.WriteLine("\nWhich area would you like to change the prize of");

                        Console.OutputEncoding = Encoding.UTF8;
                        
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[1] €" + prices.Item1.ToString("0.00"));
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n[2] €" + prices.Item2.ToString("0.00"));
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n[3] €" + prices.Item3.ToString("0.00"));
                        Console.ResetColor();

                        Console.WriteLine("\n[exit] Back");

                        while (true)
                        {
                            string choice2 = Console.ReadLine();
                            try
                            {
                                if (choice2 == "exit")
                                {
                                    break;
                                }
                                else if (Convert.ToInt32(choice2) > 0 && Convert.ToInt32(choice2) < 4)
                                {
                                    AD.UpdatePrice(Convert.ToInt32(choice), Convert.ToInt32(choice2));
                                    Tuple<double, double, double> pricesUpdated = AD.getPrices(Convert.ToInt32(choice));

                                    Console.WriteLine("Which area would you like to change the prize of\n");

                                    Console.OutputEncoding = Encoding.UTF8;

                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write("\n[1] €" + pricesUpdated.Item1.ToString("0.00"));
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("\n[2] €" + pricesUpdated.Item2.ToString("0.00"));
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\n[3] €" + pricesUpdated.Item3.ToString("0.00"));
                                    Console.ResetColor();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a option that is available");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a option that is available");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter a option that is available\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter a option that is available\n");
                }
            }
        }

        /// <summary>
        /// Deletes the line the curser is on
        /// </summary>
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// adding the movie data to the movielist
        /// </summary>
        private static void Add()
        {
            ShowData SD = new ShowData();
            ChangeData CD = new ChangeData();

            Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13) [exit] Back to menu");
            string tiyeag = Console.ReadLine();

            if (tiyeag != "exit")
            {
                // create a function for adding the movie info 
                Tuple<string, int, int> movieinfo = AddTimeYearAge(tiyeag);

                Console.WriteLine("\nPlease enter a short summary of the movie.");
                string sum = Console.ReadLine();

                Console.WriteLine("\nPlease give some actors.(Write them like this: Tom Cruise, Brad Pitt)");
                string actors = Console.ReadLine();

                CD.InsertMovie(movieinfo.Item1, movieinfo.Item2, movieinfo.Item3, sum, actors);

                // adding the movie times to the given movie
                addTime(movieinfo.Item1);
                SD.ShowMovies();
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// add a time year age and return the Tuple.
        /// </summary>
        private static Tuple<string, int, int> AddTimeYearAge(string line)
        {
            string[] TiYeAg;

            bool TiyeAg = true;
            string titel = "";
            int year = 0;
            int age = 0;

            while (TiyeAg)
            {
                try
                {
                    TiYeAg = line.Split('/');
                    titel = TiYeAg[0];
                    year = Convert.ToInt32(TiYeAg[1]);
                    age = Convert.ToInt32(TiYeAg[2]);

                    if (year <= 1800 || year > Convert.ToInt32((DateTime.Now.ToString("yyyy"))))
                    {
                        Console.WriteLine("\nPlease enter a release date that is possible. (Between 1801 and " + DateTime.Now.ToString("yyyy") + ")");
                        Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
                        line = Console.ReadLine();
                    }
                    else if (age < 0 || age > 99)
                    {
                        Console.WriteLine("\nPlease enter a age that is possible. (Between 0 and 99)");
                        Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
                        line = Console.ReadLine();
                    }
                    else
                    {
                        TiyeAg = false;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("\nEither the year or the age restriction was filled in incorrectly, please try again.\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
                    line = Console.ReadLine();
                    TiYeAg = line.Split('/');
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("\nYou missed one of the things you need to fill in, please try again.\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
                    line = Console.ReadLine();
                    TiYeAg = line.Split('/');
                }
            }

            return Tuple.Create(titel, year, age);
        }

        /// <summary>
        /// add a movie time to the given movie.
        /// </summary>
        public static void addTime(string title)
        {
            bool k = true;
            AdminData AD = new AdminData();
            while (k)
            {
                try
                {
                    Console.WriteLine("\nPlease enter a date and time when you want " + title + " to play.(" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ")");
                    string dateTime = Console.ReadLine();

                    DateTime DT = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                    if (DT < DateTime.Now)
                    {
                        Console.WriteLine("\nPlease write a date and time that is not in the past.\n");
                    }
                    else
                    {
                        int hall = 1;
                        bool y = true;
                        while (y)
                        {
                            Console.WriteLine("Please enter the theaterhall [1],[2] or [3] you want it to play in on " + dateTime);
                            string SHall = Console.ReadLine();
                            try
                            {
                                hall = Convert.ToInt32(SHall);
                                if (hall > 0 && hall < 4)
                                {
                                    y = false;
                                }
                            }
                            catch
                            {
                            }
                        }

                        DateTimeHall datetimehall1 = new DateTimeHall(DT, hall, title);

                        while (true)
                        {
                            Customer.Customer.showTime(AD.GetMovieID(title).ToString());
                            
                            //delete the last line writen
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            ClearCurrentConsoleLine();

                            Console.WriteLine("\n[add] Add another date and time\n[exit] Exit to menu");
                            string exit = Console.ReadLine();
                            if (exit == "exit")
                            {
                                k = false;
                                break;
                            }
                            else if (exit == "add")
                            {
                                break;
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please write the date and time like in the example.\n");
                }
            }
        }

        /// <summary>
        /// Display all the movies with a foreach loop, afterwards placing an ID in front of the movie to make it selectable, when selecting the ID, it'll edit the movie.
        /// </summary>
        private static void Edit()
        {
            ShowData SD = new ShowData();
            ChangeData CD = new ChangeData();
            AdminData AD = new AdminData();

            // display movies
            Console.WriteLine("\nMovies:");
            List<int> movieIDs = SD.ShowMovies();
            string ID;
            //In this loop you get the question for what to do and there are controls on the answers.
            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter the number of the movie you want to edit\n[exit] Go back to the menu.:");
                    ID = Console.ReadLine();

                    if (ID == "exit")
                    {
                        break;
                    }
                    else if (movieIDs.Contains(Convert.ToInt32(ID)))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nThe number you enter does not exist");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter 'exit' or a number that stands before a movie");
                }
            }

            try
            {
                int movieID = Convert.ToInt32(ID);
                while (true)
                {
                    Console.WriteLine("\n[1] If you want to edit an entire movie\n[2] If you only want to add a certain time\n[exit] Back to menu:");

                    // readline again
                    string option = Console.ReadLine();

                    if (option == "1")
                    {
                        string name = "";
                        int releaseDate = -1;
                        int age = -1;
                        string sum;
                        string actors;

                        Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13) or enter 'skip' if you want to skip and keep the original");
                        string tiyeag = Console.ReadLine();

                        if (tiyeag != "skip")
                        {
                            Tuple<string, int, int> movieinfo = AddTimeYearAge(tiyeag);
                            name = movieinfo.Item1;
                            releaseDate = movieinfo.Item2;
                            age = movieinfo.Item3;
                        }


                        Console.WriteLine("\nPlease enter a short summary of the movie or enter 'skip' if you want to skip and keep the original");
                        sum = Console.ReadLine();
                        if (sum == "skip")
                        {
                            sum = "";
                        }

                        Console.WriteLine("\nPlease give some actors (like this: Tom Cruise, Brad Pitt) or enter 'skip' if you want to skip and keep the original");
                        actors = Console.ReadLine();
                        if (actors == "skip")
                        {
                            actors = "";
                        }

                        CD.UpdateMovie(movieID, name, releaseDate, age, sum, actors);
                        SD.ShowMovieByID(ID);
                        break;
                    }
                    else if (option == "2")
                    {
                        addTime(AD.getTitle(movieID));
                        
                        break;
                    }
                    else if (option == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter an option that exist");
                    }
                }
            }
            catch (FormatException)
            {

            }
        }

        /// <summary>
        /// Delete the entire movie or just a time of a movie
        /// </summary>
        private static void Remove()
        {
            ShowData SD = new ShowData();
            AdminData AD = new AdminData();
            
            while (true)
            {
                try
                {
                    Console.WriteLine("\nMovies:");
                    List<int> MovieIDs = SD.ShowMovies();
                    Console.WriteLine("\n[exit] Exit to menu");
                    Console.WriteLine("\nPlease choose a movie or action you want:");
                    string choice = Console.ReadLine();
                    if (choice == "exit")
                    {
                        break;
                    }
                    else if (MovieIDs.Contains(Convert.ToInt32(choice)))
                    {
                        Console.WriteLine("\n[1] Remove the entire movie\n[2] Remove a certain time of the movie");
                        string choice2 = Console.ReadLine();
                        if (choice2 == "1")
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to delete the movie: " + AD.getTitle(Convert.ToInt32(choice)));
                            Console.WriteLine("[1] Confirm delete [2] Cancel delete");
                            while (true)
                            {
                                string choice3 = Console.ReadLine();
                                if (choice3 == "1")
                                {
                                    AD.DeleteMovie(Convert.ToInt32(choice));
                                    Console.WriteLine("\nMovies:");
                                    SD.ShowMovies();
                                    break;
                                }
                                else if (choice3 == "2")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid option");
                                }
                            }
                        }
                        else if (choice2 == "2")
                        {
                            Tuple<List<DateTime>, List<int>, List<int>> dates = Customer.Customer.showTime(choice);
                            string choice3 = Customer.Customer.selectTime(dates);

                            Console.WriteLine("Are you sure you want to delete: " + AD.getTitle(Convert.ToInt32(choice)) + "  " + dates.Item1[Convert.ToInt32(choice3)-1].ToString("HH:mm dd/MM/yyyy"));
                            Console.WriteLine("[1] Confirm delete [2] Cancel delete");
                            while (true)
                            {
                                string choice4 = Console.ReadLine();
                                if (choice4 == "1")
                                {
                                    AD.DeleteTime(dates.Item2[Convert.ToInt32(choice3)-1]);
                                    Console.WriteLine("\n" + AD.getTitle(Convert.ToInt32(choice)) + ":");
                                    Customer.Customer.showTime(choice);
                                    break;
                                }
                                else if (choice4 == "2")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid option");
                                }

                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease enter a option that stands in the menu");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter a option that stands in the menu");
                }
            }
        }

        /// <summary>
        /// Display all the movies by using a foreach loop
        /// </summary>
        private static void Display()
        {
            Console.WriteLine("\nMovies:");
            ShowData ShowMovieByInfo = new ShowData();

            List<int> MovieIDs = ShowMovieByInfo.ShowMovies();
            
            Console.WriteLine("\n[exit] Exit to menu");

            while (true)
            {
                string line = Console.ReadLine();
                try
                {
                    if (line == "exit")
                    {
                        break;
                    }
                    else if (MovieIDs.Contains(Convert.ToInt32(line)))
                    {
                        // this will return the movie details for the number you entered
                        Tuple<string, string> movieInfo = ShowMovieByInfo.ShowMovieByID(line);
                        string whichMovie = movieInfo.Item1;

                        while (true)
                        {
                            Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                            string CustomerTimeOption = Console.ReadLine();
                            if (CustomerTimeOption == "1")
                            {
                                // this will return the movie times for the movie you entered
                                //ShowMovieByInfo.ShowTimesByMovieID(whichMovie, CustomerTimeOption);
                                Tuple<List<DateTime>, List<int>, List<int>> dates = Customer.Customer.showTime(whichMovie);
                                string timeSelect = Customer.Customer.selectTime(dates);

                                if (timeSelect != "exit")
                                {
                                    Tuple<Tuple<int, int, int, int, double, double, double>, List<Tuple<double, int, int, string, bool>>> hallseatInfo = Customer.Customer.hallSeatInfo(timeSelect, dates);

                                    Customer.Customer.showHall(hallseatInfo.Item1, hallseatInfo.Item2);

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
                                Console.WriteLine("\nPlease enter an option given");
                            }
                        }
                        break;
                    }

                    else
                    {
                        Console.WriteLine("\nPlease enter an option that is in the menu");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nPlease enter an option that is in the menu");
                }
            }
        }

        /// <summary>
        /// Showing the starting menu
        /// </summary>
        public static void Menu()
        {
            ShowData SD = new ShowData();
            bool k = true;

            // test for adding some movies

            while (k)
            {
                Console.WriteLine("\nPlease enter the number that stands before the option you want.\n[1] Add a new movie.\n[2] Edit a movie or add a time\n[3] Remove a movie.\n[4] Show all the movies.\n[5] Edit hall prices\n[exit] Back to the menu.");
                string nummer = Console.ReadLine();
                if (nummer == "1")
                {
                    Add();
                }
                else if (nummer == "2")
                {
                    Edit();
                }
                else if (nummer == "3")
                {
                    Remove();
                }
                else if (nummer == "4")
                {
                    Display();
                }
                else if (nummer == "5")
                {
                    editPrice();
                }
                else if (nummer == "exit")
                {
                    k = false;
                    break;
                }
            }
        }
    }
}
