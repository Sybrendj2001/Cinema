using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.Employee;
using CinemaConsole.Data.BackEnd;
using System.IO;
using System.Globalization;

namespace CinemaConsole.Data.BackEnd
{
    public class TicketInfo
    {
        private string Owner { get; set; }

        private int X { get; set; }

        private int Y { get; set; }

        private double Price { get; set; }

        private DateTime Time { get; set; }

        private string MovieName { get; set; }

        private string TheatherHall { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketowner"></param>
        /// <param name="seatx"></param>
        /// <param name="seaty"></param>
        /// <param name="seatprice"></param>
        /// <param name="time"></param>
        /// <param name="moviename"></param>
        /// <param name="theatherhall"></param>
        public TicketInfo(string ticketowner, int seatx, int seaty, double seatprice, 
                            string time, string moviename, int theatherhall)
        {
            Owner = ticketowner;
            X = seatx;
            Y = seaty;
            Price = seatprice;
            Time = DateTime.ParseExact(time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            MovieName = moviename;
            TheatherHall = theatherhall.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SeatX"></param>
        /// <param name="SeatY"></param>
        /// <param name="Time"></param>
        /// <param name="MovieName"></param>
        /// <param name="TheatherHall"></param>
        /// <returns></returns>
        private string createTicketID(int SeatX, int SeatY,DateTime Time, string MovieName, string TheatherHall)
        {
            //Split the data what time the movie starts
            int MovieStartTimeMinute = Time.Minute;
            int MovieStartTimeHour = Time.Hour;
            int MovieStartTimeDay = Time.Day;
            int MovieStartTimeMonth = Time.Month;
            int MovieStartTimeYear = Time.Year;
            //Takes the first 3 letters of the movie and makes them all caps
            string MovieNameShort = MovieName.Substring(0, 3).ToUpper();

            //Create the movie unique id
            string MovieTicketData = MovieStartTimeMinute + MovieStartTimeHour + MovieStartTimeDay +
                MovieStartTimeMonth + MovieStartTimeYear + MovieNameShort + SeatX + SeatY + TheatherHall;

            return MovieTicketData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketID"></param>
        private void SaveTicket(string ticketID)
        {
            string targetFile = "TicketSafe.txt";
            string fullPath = Path.GetFullPath(targetFile);
            StreamWriter file = new StreamWriter(fullPath);
            file.WriteLine(ticketID + ".");
            file.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchedTicket"></param>
        /// <returns></returns>
        private bool TicketExists(string searchedTicket)
        {
            bool ticketDoesExist = false;
            string targetFile = "TicketSafe.txt";
            string fullPath = Path.GetFullPath(targetFile);
            StreamReader file = new StreamReader(fullPath);
            string CompleteFileRaw = file.ReadToEnd();
            string[] CompleteFile = CompleteFileRaw.Split('.');
            foreach (string item in CompleteFile)
            {
                if (item.Contains(searchedTicket))
                {
                    ticketDoesExist = true;
                }
            }
            file.Close();
            return ticketDoesExist;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Menu()
        {
            bool checkingTicket = true;
            string ticketInfo = "";
            while (checkingTicket)
            {
                Console.WriteLine("[1]Create a ticket.\n[2]Check ticket info.\n[3]Confirm the ticket.\n[4]Check if a ticket exists.\n[5]Exit.");
                string todo = Console.ReadLine();
                switch (todo)
                {
                    case "1":
                        ticketInfo = createTicketID(X,Y,Time,MovieName,TheatherHall);
                        Console.WriteLine("Completed");
                        break;
                    case "2":
                        Console.WriteLine(ticketInfo + " is the current ticketcode.");
                        break;
                    case "3":
                        if (ticketInfo == "")
                        {
                            Console.WriteLine("You havent created a ticket yet");
                        }
                        else
                        { 
                            SaveTicket(ticketInfo);
                            Console.WriteLine("Ticket saved");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Write the ticketcode you want to search:");
                        string ticketToCheck = Console.ReadLine();
                        bool check = TicketExists(ticketToCheck);
                        if (check)
                        {
                            Console.WriteLine("Your ticket exists.");
                        }
                        else
                        {
                            Console.WriteLine("Your ticket doesn't exist.");
                        }
                        break;
                    case "5":
                        checkingTicket = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Input!");
                        break;
                }
            }
        }
    }
}
