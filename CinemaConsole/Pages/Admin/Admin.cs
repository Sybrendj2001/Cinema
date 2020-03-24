using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data.Employee;

namespace CinemaConsole.Pages.Admin
{
    public class Admin : Employee
    {
        public Admin()
        {
            
        }

        /// <summary>
        /// reading the movie data, then splitting the data with  "/" and adding it to the movielist
        /// </summary>
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
            }
                    
            Console.WriteLine("Please enter a short summary of the movie.");
            string sum = Console.ReadLine();

            Console.WriteLine("Please give some actors.(Write them like this: Tom Cruise, Brad Pitt)");
            string actors = Console.ReadLine();

            int id = MovieList.movieList.Count;
            Movies movie = new Movies(titel, year, age, sum, actors);


            MovieList.movieList.Add(movie);
        }
        /// <summary>
        /// Display all the movies with a foreach loop, afterwards placing an ID in front of the movie to make it selectable, when selecting the ID, it'll remove the movie.
        /// </summary>
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
            Customer.Customer.AddStuff();

            while (k)
            {
                Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Add a new movie.\n[2] Remove a movie.\n[3] Show all the movies.\n[4] Exit the program.");
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
                }
            }
        }
    }
}
