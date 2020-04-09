using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data;

namespace CinemaConsole.Pages.Admin
{
    public class Admin : Employee
    {
        public Admin()
        {
            
        }

        /// <summary>
        /// adding the movie data to the movielist
        /// </summary>
        private static void Add()
        {
            Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13) [exit] Back to menu");
            string tiyeag = Console.ReadLine();

            if (tiyeag != "exit")
            {
                // create a function for adding the movie info 
                var movieinfo = AddTimeYearAge(tiyeag);

                Console.WriteLine("Please enter a short summary of the movie.");
                string sum = Console.ReadLine();

                Console.WriteLine("Please give some actors.(Write them like this: Tom Cruise, Brad Pitt)");
                string actors = Console.ReadLine();

                Movies movie = new Movies(movieinfo.Item1, movieinfo.Item2, movieinfo.Item3, sum, actors);

                MovieList.movieList.Add(movie);

                // adding the movie times to the given movie
                AddMovieTimes(movie);
                MovieList.orderList();
            }
        }

        /// <summary>
        /// add a time year age and return the Tuple.
        /// </summary>
        private static Tuple<string, int, int> AddTimeYearAge(string line)
        {
            string[] TiYeAg = line.Split('/');

            bool TiyeAg = true;
            string titel = "";
            int year = 0;
            int age = 0;

            while (TiyeAg)
            {
                try
                {
                    titel = TiYeAg[0];
                    year = Convert.ToInt32(TiYeAg[1]);
                    age = Convert.ToInt32(TiYeAg[2]);
                    TiyeAg = false;
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
        private static void AddMovieTimes(Movies movie)
        {
            bool k = true;
            while (k)
            {
                Console.WriteLine("Please enter a date and time when you want " + movie.getMovieInfo().Item2 + " to play.(12/12/2012 12:20)");
                string dateTime = Console.ReadLine();

                string[] DateTime = dateTime.Split(' ');
                string date = DateTime[0];
                string time = DateTime[1];

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
                DateTimeHall datetimehall = new DateTimeHall(date, time, hall, movie);

                movie.DateTimeHallsList.Add(datetimehall);
                movie.orderList();

                Console.WriteLine("Please enter 'add' if you have no more dates or times to fill in or press enter to continue.");
                string exit = Console.ReadLine();
                if (exit == "add")
                {
                    k = false;
                }
            }
        }
        
        /// <summary>
        /// Display all the movies with a foreach loop, afterwards placing an ID in front of the movie to make it selectable, when selecting the ID, it'll edit the movie.
        /// </summary>
        private static void Edit()
        {
            bool k = true;
            while (k)
            {
                Console.WriteLine("Movies:");
                // Loop trough all movies currently in the movielist
                foreach (Movies movie in MovieList.movieList)
                {
                    Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                }

                // count all movies + 1 for an exit number
                int moviecount = MovieList.movieList.Count + 1;

                Console.WriteLine("Enter the number of the movie you want to edit or enter [exit] to go back.:");

                string line = Console.ReadLine();

                if (line == "exit")
                {
                    k = false;
                }

                foreach (Movies movie in MovieList.movieList)
                {
                    // check if number equals movie ID
                    if (line == movie.getMovieInfo().Item1.ToString())
                    {
                        // save line as an int
                        int number = Int32.Parse(line);
                        Console.WriteLine("[1] If you want to edit an entire movie\n[2] If you only want to add a certain time\n[exit] Back to menu:");

                        // readline again
                        line = Console.ReadLine();

                        if (line == "1")
                        {
                            Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13) or enter 'skip' if you want to skip and keep the original");
                            string tiyeag = Console.ReadLine();

                            if (tiyeag != "skip")
                            {
                                var movieinfo = AddTimeYearAge(tiyeag);

                                // replace original values for new ones
                                movie.setMovieInfo(movieinfo.Item1, movieinfo.Item2, movieinfo.Item3);
                            }

                            Console.WriteLine("Please enter a short summary of the movie or enter 'skip' if you want to skip and keep the original");
                            string sum = Console.ReadLine();
                            if (sum != "skip")
                            {
                                // replace original value for new one
                                movie.setMovieInfo("",0, 0, sum);
                            }

                            Console.WriteLine("Please give some actors (like this: Tom Cruise, Brad Pitt) or enter 'skip' if you want to skip and keep the original");
                            string actors = Console.ReadLine();
                            if (actors != "skip")
                            {
                                // replace original value for new one
                                movie.setMovieInfo("", 0, 0, "", actors);
                            }

                            // i have to break out of the foreach loop, because you cannot modify a loop while you're in it. 
                            break;
                            
                        }
                        else if (line == "2")
                        {
                            AddMovieTimes(movie);
                            break;
                        }
                        else if (line == "exit")
                        {
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Display all the movies with a foreach loop, afterwards placing an ID in front of the movie to make it selectable, when selecting the ID, it'll remove the movie.
        /// </summary>
        private static void Remove()
        {
            bool k = true;
            while (k)
            {
                Console.WriteLine("Movies:");
                // Loop trough all movies currently in the movielist
                foreach (Movies movie in MovieList.movieList)
                {
                    Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                }

                // count all movies + 1 for an exit number
                int moviecount = MovieList.movieList.Count + 1;

                Console.WriteLine("Enter the number of the movie you want to remove or enter [exit] to go back:");
                
                string line = Console.ReadLine();

                if (line == "exit")
                {
                    k = false;
                }

                foreach (Movies movie in MovieList.movieList)
                {
                    // check if number equals movie ID
                    if (line == movie.getMovieInfo().Item1.ToString())
                    {
                        // save line as an int
                        int number = Int32.Parse(line);
                        Console.WriteLine("[1] If you want to remove the entire movie \n[2] If you only want to remove a certain time\n[exit] Back to overview:");
                        
                        // readline again
                        line = Console.ReadLine();

                        if (line == "1")
                        {
                            // remove movie if id is the same as user input
                            MovieList.movieList.RemoveAll(movie1 => movie1.getMovieInfo().Item1 == (number));
                            Console.WriteLine("You removed " + movie.getMovieInfo().Item2);

                            Console.WriteLine("Press enter to continue");

                            // using readline here to wait for an enter
                            Console.ReadLine();

                            // i have to break out of the foreach loop, because you cannot modify a loop while you're in it. 
                            break;
                        }
                        else if (line == "2")
                        {
                            Console.WriteLine("Select the time you want to remove:");
                            foreach (DateTimeHall date in movie.DateTimeHallsList)
                            {
                                Console.WriteLine("[" + date.getDateInfo().Item1 + "] " + date.getDateInfo().Item2 + "      " + date.getDateInfo().Item3);
                            }

                            // make an int of the input
                            int time = int.Parse(Console.ReadLine());
                            movie.DateTimeHallsList.RemoveAll(movie1 => movie1.getDateInfo().Item1 == (time));

                            Console.WriteLine("Press enter to continue");

                            // using readline here to wait for an enter
                            Console.ReadLine();

                            // i have to break out of the foreach loop, because you cannot modify a loop while you're in it. 
                            break;
                        }
                        else if (line == "exit")
                        {
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        
        /// <summary>
        /// Display all the movies by using a foreach loop
        /// </summary>
        private static void Display()
        {
            Console.WriteLine("Movies:");
            // Loop trough all movies currently in the movielist
            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                
                foreach (DateTimeHall date in movie.DateTimeHallsList)
                {
                    Console.WriteLine(date.getDateInfo().Item1 + "      " + date.getDateInfo().Item2 + "     " + date.getDateInfo().Item3 + "    Theaterhall " + date.getDateInfo().Item4.getHallInfo().Item2);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Press enter to continue");
            
            // using readline here to wait for an enter
            Console.ReadLine();
        }
        
        /// <summary>
        /// Showing the starting menu
        /// </summary>
        public static void Menu()
        {
            bool k = true;

            // test for adding some movies

            while (k)
            {
                Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Add a new movie.\n[2] Edit a movie or add a time\n[3] Remove a movie.\n[4] Show all the movies.\n[exit] Back to the menu.");
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
                else if (nummer == "exit")
                {
                    k = false;
                    break;
                }
            }
        }
    }
}
