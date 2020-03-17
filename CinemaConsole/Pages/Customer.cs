using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;
using CinemaConsole.Data;


namespace CinemaConsole.Pages
{
    public class Customer
    {
        public string MovieTimeId { get; set; }

        public string MovieDate { get; set; }
        public string MovieTime { get; set; }

        public override string ToString()
        {
            return "ID: " + MovieTimeId + "   Date: " + MovieDate + "   Time: " + MovieTime;
        }


        public static void Main()
        {
            // Create a list of times.
            List<Time> times = new List<Time>();

            // Add times and dates to the list.
            times.Add(new Time { MovieTimeId = 0, MovieDate = 12 - 04, MovieTime = 12.00 });
            times.Add(new Time { MovieTimeId = 1, MovieDate = 12 - 04, MovieTime = 18.30 });
            times.Add(new Time { MovieTimeId = 2, MovieDate = 12 - 04, MovieTime = 21.00 });
            times.Add(new Time { MovieTimeId = 3, MovieDate = 13 - 04, MovieTime = 19.15 });
            times.Add(new Time { MovieTimeId = 4, MovieDate = 13 - 04, MovieTime = 21.00 });
            times.Add(new Time { MovieTimeId = 5, MovieDate = 14 - 04, MovieTime = 19.30 });


            // Write out the times and dates in the list. This will call the overridden ToString method
            // in the Time class.
            Console.WriteLine();
            foreach (Time aTime in times)
            {
                Console.WriteLine(aTime);
            }

            string TimeChosen;
            Console.Write("Enter Time ID ");
            TimeChosen = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", TimeChosen);


        }

    }
}