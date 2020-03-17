using System;
using System.Collections.Generic;


namespace CinemaConsole.Pages
{

    public class Customer
    {
        public class Movie : IEquatable<Movie>
        {
            public string MovieTime { get; set; }

            public string MovieDate { get; set; }

            public int MovieId { get; set; }

            public override string ToString()
            {
                return "ID: " + MovieId + "   Datum: " + MovieDate + "   Time: " + MovieTime;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Movie objAsMovie = obj as Movie;
                if (objAsMovie == null) return false;
                else return Equals(objAsMovie);
            }
            public override int GetHashCode()
            {
                return MovieId;
            }
            public bool Equals(Movie other)
            {
                if (other == null) return false;
                return (this.MovieId.Equals(other.MovieId));
            }
        }
        public class Example
        {
            public static void Main()
            {
                // Create a list of times.
                List<Movie> agenda = new List<Movie>();

                // Add Time and Dates to the list.
                agenda.Add(new Movie { MovieTime = "12:30", MovieDate = "12-06-2020", MovieId = 1 });
                agenda.Add(new Movie { MovieTime = "19:45", MovieDate = "12-06-2020", MovieId = 2 });
                agenda.Add(new Movie { MovieTime = "18:00", MovieDate = "13-06-2020", MovieId = 3 });
                agenda.Add(new Movie { MovieTime = "21:30", MovieDate = "13-06-2020", MovieId = 4 });
                agenda.Add(new Movie { MovieTime = "15:30", MovieDate = "14-06-2020", MovieId = 5 });
                agenda.Add(new Movie { MovieTime = "21:00", MovieDate = "14-06-2020", MovieId = 6 });

                // Write out the time and dates in the list. This will call the overridden ToString method
                // in the Movie class.
                Console.WriteLine();
                foreach (Movie aMovie in agenda)
                {
                    Console.WriteLine(aMovie);
                }
                Console.WriteLine(" ");

                Console.WriteLine("Please enter yout choice. Type in ID: ");
                string CustomerTimeDate = Console.ReadLine();




            }
        }
    }
}