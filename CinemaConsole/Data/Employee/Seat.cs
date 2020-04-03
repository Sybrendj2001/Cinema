using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
    public class Seat
    {
        private string SeatName { get; set; }
        private string SeatPlace { get; set; }
        private bool SeatAvail { get; set; } = true;
        private double SeatPrice { get; set; } = 15.00;

        public Seat(string Sname, string Splace, bool Savail)
        {
            SeatPlace = Splace;
            SeatName = Sname;
            SeatAvail = Savail;
        }

        public void editAvail()
        {
            SeatAvail = !SeatAvail;
        }

        public Tuple<string, string, bool, double> getInfo()
        {
            return Tuple.Create(SeatPlace, SeatName, SeatAvail, SeatPrice);
        }
    }
}
