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

        private static void Display()
        {
            Console.WriteLine("Movies:");
            for (int i = 0; i < MovieList.movieList.Count; i++)
            {
                Console.WriteLine(MovieList.movieList[i].getMovieInfo().Item1 + "   " + MovieList.movieList[i].getMovieInfo().Item2);
            }

        }

        public static void Menu()
        {
            bool k = true;

            while (k)
            {
                Console.WriteLine("PLease enter the number that stands before the option you want.\n[1] Add a new movie.\n[2] Show all the movies.\n[3] Exit the program.");
                string nummer = Console.ReadLine();
                if (nummer == "1")
                {
                    Add();
                }
                else if (nummer == "2")
                {
                    Display();
                }
                else if (nummer == "3")
                {
                    k = false;
                    break;
                }

            }
        }
    }
}
