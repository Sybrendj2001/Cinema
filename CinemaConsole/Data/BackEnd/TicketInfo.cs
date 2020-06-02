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
        /// <summary>The name of the owner of the ticket</summary>
        private string Owner { get; set; }

        /// <summary>The X value the ticket reservation starts at</summary>
        private int X { get; set; }

        /// <summary>The Y value the ticket reservation starts at</summary>
        private int Y { get; set; }

        /// <summary>The total price of the tickets</summary>
        private double Price { get; set; }

        /// <summary>The time the movie starts</summary>
        private DateTime Time { get; set; }

        /// <summary>The name of the movie</summary>
        private string MovieName { get; set; }

        /// <summary>The theatherhall the movie is played in</summary>
        private int TheatherHall { get; set; }

        /// <summary>Thge ID of the ticket</summary>
        private string Ticket { get; set; }

        /// <summary>The Email of the owner</summary>
        private string Email { get; set; }

        /// <summary>The amount of tickets that is reserved</summary>
        private int Amount { get; set; }

        /// <summary>
        /// Initiliazes all the info about the ticket
        /// </summary>
        /// <param name="TicketOwner">The owner of the ticket</param>
        /// <param name="SeatX">The X the reservation starts</param>
        /// <param name="SeatY">The Y the reservation starts</param>
        /// <param name="SeatPrice">The total price of </param>
        /// <param name="StartTime">The time the movie starts</param>
        /// <param name="Name">The Name of the movie</param>
        /// <param name="Hall">The hall the movie is played in</param>
        public TicketInfo(string TicketOwner, string EmailAddress, int SeatX, int SeatY, int AmountOfSeats, double SeatPrice, 
                            string StartTime, string Name, int Hall)
        {
            Owner = TicketOwner;
            X = SeatX;
            Y = SeatY;
            Price = SeatPrice;
            Time = DateTime.ParseExact(StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            MovieName = Name;
            TheatherHall = Hall;
            Ticket = createTicketID();
            Email = EmailAddress;
            Amount = AmountOfSeats;
        }

        /// <summary>
        /// Creates the ID for the ticket
        /// </summary>
        /// <returns>The ID that is created</returns>
        private string createTicketID()
        {
            //Takes the first 3 letters of the movie and makes them all caps
            string MovieNameShort = MovieName.Substring(0, 3).ToUpper();

            //Create the movie unique id
            string MovieTicketData = (Time.ToString("mm")) + (Time.ToString("HH")) + (Time.ToString("dd")) +
                (Time.ToString("MM")) + (Time.ToString("yyyy")) + MovieNameShort + X + Y + TheatherHall;

            return MovieTicketData;
        }

        //TODO: Word dit ergens gebruikt?
        public Tuple<Tuple<string, string, string, string>, int, int, int, double, DateTime, int> GetTicketInfo()
        {
            return Tuple.Create(Tuple.Create(Owner, Email, MovieName, Ticket), X, Y, Amount, Price, Time, TheatherHall);
        }
    }
}