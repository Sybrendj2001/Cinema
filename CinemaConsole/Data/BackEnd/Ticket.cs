using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.BackEnd
{
    public class Ticket
    {
        private string TicketOwner { get; set; }

        public string TicketNumber { get; set; }

        private int SeatX { get; set; }

        private int SeatY { get; set; }

        private double SeatPrice { get; set; }

        private DateTime MovieDate { get; }

        public Ticket(string owner, string number, int x, int y, double price, DateTime date)
        {
            TicketOwner = owner;
            TicketNumber = number;
            SeatX = x;
            SeatY = y;
            SeatPrice = price;
            MovieDate = date;
        }
    }
}
