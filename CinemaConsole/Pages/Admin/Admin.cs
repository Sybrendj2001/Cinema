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
                catch (FormatException e)
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

            Movies movie = new Movies(titel, year, age, sum, actors);


            MovieList.movieList.Add(movie);
        }

        private static void Display()
        {
            Console.WriteLine("Movies:");
            for (int i = 0; i < MovieList.movieList.Count; i++)
            {
                Console.WriteLine(MovieList.movieList[i].getInfo().Item1 + "   " + MovieList.movieList[i].getInfo().Item2);
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
                }

            }
        }
    }
}
