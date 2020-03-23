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
        // adding some movies to test
        public static void AddStuff()
        {
            Movies movie1 = new Movies("Transformers", 2007, 12, "An ancient struggle between two Cybertronian races, the heroic Autobots and the evil Decepticons, comes to Earth, with a clue to the ultimate power held by a teenager.", "Shia LaBeouf, Megan Fox");
            MovieList.movieList.Add(movie1);
            Movies movie2 = new Movies("Avengers", 2012, 12, "Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.", "Robert Downey Jr., Chris Evans, Scarlett Johansson");
            MovieList.movieList.Add(movie2);
            Movies movie3 = new Movies("The Dark Knight", 2008, 12, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", " Christian Bale, Heath Ledger, Aaron Eckhart");
            MovieList.movieList.Add(movie3);
        }
        private static void Display()
        {
            while (true)
            {

                // convert movielist count to a string
                string movieCount = (MovieList.movieList.Count + 1).ToString();

                Console.WriteLine("\nPlease enter the number that stands before the movie you want to reserve.");
                Console.WriteLine("Movies on today:");

                // Loop trough all movies currently in the movielist
                foreach (Movies movie in MovieList.movieList)
                {
                    Console.WriteLine("[" + movie.getMovieInfo().Item1 + "]   " + movie.getMovieInfo().Item2 + " (" + movie.getMovieInfo().Item3 + ")");
                }

                // check if user wants to go back
                Console.WriteLine("\n[" + movieCount + "] Back to the menu.");

                string line = Console.ReadLine();

                // check if user wants to go back 
                if (line == movieCount.ToString())
                {
                    break;
                }


                for (int i = 0; i < MovieList.movieList.Count; i++)
                {
                    // check if number equals movie ID
                    if (line == MovieList.movieList[i].getMovieInfo().Item1.ToString())
                    {
                        Console.WriteLine("Movie selected: " + MovieList.movieList[i].getMovieInfo().Item2);
                        Console.WriteLine("Year: " + MovieList.movieList[i].getMovieInfo().Item3);
                        Console.WriteLine("Age restriction: " + MovieList.movieList[i].getMovieInfo().Item4 + "+");
                        Console.WriteLine("Actors: " + MovieList.movieList[i].getMovieInfo().Item6);
                        Console.WriteLine("Summary: " + MovieList.movieList[i].getMovieInfo().Item5);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Show all the movies.\n[2] Exit the program.");
                string line = Console.ReadLine();
                if (line == "1")
                {
                    AddStuff();
                    Display();
                }
                else if (line == "2")
                {
                    break;
                }
            }
        }
    }
}
