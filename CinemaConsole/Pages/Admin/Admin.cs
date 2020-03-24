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

        private static void Add()
        {
            Console.WriteLine("\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
            string tiyeag = Console.ReadLine();

            string[] TiYeAg = tiyeag.Split('/');

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
                    tiyeag = Console.ReadLine();
                    TiYeAg = tiyeag.Split('/');
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("\nYou missed one of the things you need to fill in, please try again.\nPlease enter the Titel/year/age restriction. (IronMan/2008/13)");
                    tiyeag = Console.ReadLine();
                    TiYeAg = tiyeag.Split('/');
                }
            }
                    
            Console.WriteLine("Please enter a short summary of the movie.");
            string sum = Console.ReadLine();

            Console.WriteLine("Please give some actors.(Write them like this: Tom Cruise, Brad Pitt)");
            string actors = Console.ReadLine();

            int id = MovieList.movieList.Count;
            Movies movie = new Movies(titel, year, age, sum, actors);


            MovieList.movieList.Add(movie);

            bool k = true;

            while (k)
            {
                Console.WriteLine("Please enter a date and time when you want the movie to play.(12-12-2012/12:20)");
                string dateTime = Console.ReadLine();
                string[] DateTime = dateTime.Split('/');
                string date = DateTime[0];
                string time = DateTime[1];

               

                int hall = 1;
                bool y = true;
                while (y)
                {
                    Console.WriteLine("Please enter theaterhall you want it to play in.(1,2,3)");
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
                DateTimeHall datetimehall = new DateTimeHall(date, time, hall);

                movie.DateTimeHallsList.Add(datetimehall);

                Console.WriteLine("Please type in add if you have no more dates or times to fill in. Else press on enter");
                string exit = Console.ReadLine();
                if (exit == "add")
                {
                    k = false;
                }

            }
        }

        private static void Remove()
        {
            Console.WriteLine("Movies:");
            // Loop trough all movies currently in the movielist
            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
            }
            Console.WriteLine("Enter the number of the movie you want to remove:");
            string line = Console.ReadLine();
            int number = Int32.Parse(line);

            foreach (Movies movie in MovieList.movieList)
            {
                // check if number equals movie ID
                if (line == movie.getMovieInfo().Item1.ToString())
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
                else
                {
                    continue;
                }
            }
        }

        private static void Display()
        {
            Console.WriteLine("Movies:");
            // Loop trough all movies currently in the movielist
            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                
                foreach (DateTimeHall date in movie.DateTimeHallsList)
                {
                    Console.WriteLine(date.getInfo().Item1 + "      " + date.getInfo().Item2 + "    Theaterhall " + date.getInfo().Item3.getInfo().Item2);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Press enter to continue");
            
            // using readline here to wait for an enter
            Console.ReadLine();
        }

        public static void Menu()
        {
            bool k = true;

            // test for adding some movies
            Customer.Customer.AddStuff();

            while (k)
            {
                Console.WriteLine("PLease enter the number that stands before the option you want.\n[1] Add a new movie.\n[2] Remove a movie.\n[3] Show all the movies.\n[4] Exit the program.");
                string nummer = Console.ReadLine();
                if (nummer == "1")
                {
                    Add();
                }
                else if (nummer == "2")
                {
                    Remove();
                }
                else if (nummer == "3")
                {
                    Display();
                }
                else if (nummer == "4")
                {
                    k = false;
                    break;
                }
            }
        }
    }
}
