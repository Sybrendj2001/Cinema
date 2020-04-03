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

        private int TheatherHall { get; set; }

        private string Ticket { get; set; }

        private string Email { get; set; }

        private int Amount { get; set; }


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
        public TicketInfo(string ticketowner, string email, int seatx, int seaty, int amount, double seatprice, 
                            string time, string moviename, int theatherhall)
        {
            Owner = ticketowner;
            X = seatx;
            Y = seaty;
            Price = seatprice;
            Time = DateTime.ParseExact(time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            MovieName = moviename;
            TheatherHall = theatherhall;
            Ticket = createTicketID();
            Email = email;
            Amount = amount;
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
        private string createTicketID()
        {
            //Takes the first 3 letters of the movie and makes them all caps
            string MovieNameShort = MovieName.Substring(0, 3).ToUpper();

            //Create the movie unique id
            string MovieTicketData = (Time.ToString("mm")) + (Time.ToString("HH")) + (Time.ToString("dd")) +
                (Time.ToString("MM")) + (Time.ToString("yyyy")) + MovieNameShort + X + Y + TheatherHall;

            return MovieTicketData;
        }

        public Tuple<Tuple<string, string, string, string>, int, int, int, double, DateTime, int> GetTicketInfo()
        {
            return Tuple.Create(Tuple.Create(Owner, Email, MovieName, Ticket), X, Y, Amount, Price, Time, TheatherHall);
        }


    }
}
