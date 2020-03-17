using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.Employee;


namespace CinemaConsole.Pages.Customer
{
    public class Customer
    {
        public static void AddStuff()
        {
            Movies movie1 = new Movies(1, "Transformers", 2018, 12, "bla", "bla1");
            Movies movie2 = new Movies(2, "Avengers", 2012, 12, "bla", "bla2");
            Movies movie3 = new Movies(3, "Batman", 2010, 12, "bla", "bla3");

            MovieList.movieList.Add(movie1);
            MovieList.movieList.Add(movie2);
            MovieList.movieList.Add(movie3);

        }
        private static void Display()
        {

            // convert movielist count to a string
            string movieCount = (MovieList.movieList.Count + 1).ToString();

            Console.WriteLine("Please enter the number that stands before the movie you want to reserve.");
            Console.WriteLine("Movies on today:");

            // Loop trough all movies currently in the movielist
            foreach (Movies movie in MovieList.movieList)
            {
                Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " " + movie.getMovieInfo().Item3);
            }
            
            Console.WriteLine("\n[" + movieCount + "] Exit the program.");

            // Loop trough all movies to check which number was selected
            foreach (Movies movie in MovieList.movieList)
            {
                // check if number equals movie ID
                if (Console.ReadLine() == movie.getMovieInfo().Item1.ToString())
                {
                    Console.WriteLine("Movie selected: " + movie.getMovieInfo().Item2);
                    Console.WriteLine("Info: " + movie.getMovieInfo().Item3);
                }
            }
            // check if user wants to exit 
            if (Console.ReadLine() == movieCount)
            {
                // clear the console and load the menu (not working yet)
                Console.Clear();
                Menu();
            }
        }

        public static void Menu()
        {
            bool k = true;

            while (k)
            {
                Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Show all the movies.\n[2] Exit the program.");
                string nummer = Console.ReadLine();
                if (nummer == "1")
                {
                    Display();
                }
                else if (nummer == "2")
                {
                    k = false;
                }
            }
        }
    }
}
