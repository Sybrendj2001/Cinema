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

                // Add Time and Dates to the list. (For now)
                agenda.Add(new Movie { MovieTime = "12:30", MovieDate = "12-06-2020", MovieId = 1 });
                agenda.Add(new Movie { MovieTime = "19:45", MovieDate = "12-06-2020", MovieId = 2 });
                agenda.Add(new Movie { MovieTime = "18:00", MovieDate = "13-06-2020", MovieId = 3 });
                agenda.Add(new Movie { MovieTime = "21:30", MovieDate = "13-06-2020", MovieId = 4 });
                agenda.Add(new Movie { MovieTime = "15:30", MovieDate = "14-06-2020", MovieId = 5 });
                agenda.Add(new Movie { MovieTime = "21:00", MovieDate = "14-06-2020", MovieId = 6 });


                bool k = true;
                // menu with options for the customer to choose from.
                while (k)
                {

                    Console.WriteLine("\n[1] Pick date and time\n[2] Contact information\n[3] Exit the program\n\nPlease enter your choice. Type in ID: ");
                 
                    string CustomerOption = Console.ReadLine();

                    if (CustomerOption == "1")
                    {
                        foreach (Movie aMovie in agenda)
                        {
                            Console.WriteLine(aMovie);
                        }

                        Console.WriteLine("\nPlease enter yout choice. Type in ID: ");
                        int CustomerTimeDate = int.Parse(Console.ReadLine());

                        //Out of range check
                        if (CustomerTimeDate > agenda.Count)
                        {
                            Console.WriteLine("\nError ID " + CustomerTimeDate + " does not exist");
                        }

                        else
                        {
                            Console.WriteLine("\nYou have chosen for " + agenda[CustomerTimeDate - 1]);
                        }
                    }

                    //Cinema contact information
                    if (CustomerOption == "2")
                    {
                        Console.WriteLine("\nAdres: Wijnhaven 99, 3011 WN Rotterdam\nPhone number: 010-794 4000\n\nOpening hours:\nMonday - Thursday: 12:00 - 21:00\nFriday: 12:00 - 01:00\nSaturday - Sunday: 12:00 - 02:00 ");
                    }

                    //Exit the menu 
                    else if(CustomerOption == "3")
                    {
                        k = false;
                    }

                }

            }
        }
    }
}