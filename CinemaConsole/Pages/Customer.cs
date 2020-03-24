using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data;
using CinemaConsole.Data.Employee;


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
            
            DateTimeHall datetimehall1 = new DateTimeHall("21-04-2020", "12:20", 1, movie1);
            movie1.DateTimeHallsList.Add(datetimehall1);
            DateTimeHall datetimehall1A = new DateTimeHall("21-06-2020", "12:20", 2, movie1);
            movie1.DateTimeHallsList.Add(datetimehall1A);

            Movies movie2 = new Movies("Avengers", 2012, 12, "Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.", "Robert Downey Jr., Chris Evans, Scarlett Johansson");
            MovieList.movieList.Add(movie2);
            
            DateTimeHall datetimehall2 = new DateTimeHall("21-05-2020", "12:20", 1, movie2);
            movie2.DateTimeHallsList.Add(datetimehall2);
            DateTimeHall datetimehall2A = new DateTimeHall("21-06-2020", "12:20", 3, movie2);
            movie2.DateTimeHallsList.Add(datetimehall2A);

            Movies movie3 = new Movies("The Dark Knight", 2008, 12, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", " Christian Bale, Heath Ledger, Aaron Eckhart");
            MovieList.movieList.Add(movie3);
            
            DateTimeHall datetimehall3 = new DateTimeHall("21-06-2020", "12:20", 1, movie3);
            movie3.DateTimeHallsList.Add(datetimehall3);
            DateTimeHall datetimehall3A = new DateTimeHall("21-05-2020", "12:20", 2, movie3);
            movie3.DateTimeHallsList.Add(datetimehall3A);
        }


        private static void Reserve(Movies movie)
        {
            Console.WriteLine("Movie selected: " + movie.getMovieInfo().Item2);
            Console.WriteLine("Year: " + movie.getMovieInfo().Item3);
            Console.WriteLine("Age restriction: " + movie.getMovieInfo().Item4 + "+");
            Console.WriteLine("Actors: " + movie.getMovieInfo().Item6);
            Console.WriteLine("Summary: " + movie.getMovieInfo().Item5);

            Console.WriteLine("\nWould you like to see the dates and times? [1] Yes, [2] No:");
            string CustomerReservateOption = Console.ReadLine();

            if (CustomerReservateOption == "1")
            {
                int i = 1;
                foreach (DateTimeHall date in movie.DateTimeHallsList)
                { 
                    Console.WriteLine("[" + i + "] "+ date.getInfo().Item1 + "      " + date.getInfo().Item2);
                    i++;
                }

                Console.WriteLine("\nWould you like to reserve? [1] Yes, [2] No:");



            }

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

                foreach (Movies aMovie in MovieList.movieList) 
                {
                    if (line == aMovie.getMovieInfo().Item1.ToString())
                    {
                        Reserve(aMovie);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Showing the starting menu
        /// </summary>
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Please enter the number that stands before the option you want.\n[1] Show all the movies.\n[2] Show contact information\n[3] Exit the program.");
                string line = Console.ReadLine();
                if (line == "1")
                {
                    AddStuff();
                    Display();
                }

                else if (line == "2")
                {
                    Console.WriteLine("\nAdres: Wijnhaven 99, 3011 WN Rotterdam\nPhone number: 010-794 4000\n\nOpening hours:\nMonday - Thursday: 12:00 - 21:00\nFriday: 12:00 - 01:00\nSaturday - Sunday: 12:00 - 02:00 \n\n");
                }

                else if (line == "3")
                {
                    break;
                }
            }
        }
    }
}

