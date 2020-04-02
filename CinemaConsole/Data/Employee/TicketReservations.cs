using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
	class TicketReservations
	{
		private string movieTitle { get; set; }
		private string date_time { get; set; }
		private string name { get; set; }
		private string seats { get; set; }
		private string email { get; set; }

		public TicketReservations(string customerMovieTitle, string customerDate_time, string customerSeats, string customerName, string customerEmail)
		{
			movieTitle = customerMovieTitle;
			date_time = customerDate_time;
			seats = customerSeats;
			name = customerName;
			email = customerEmail;
		}

		public Tuple<string, string, string, string, string> getReservationInfo()
		{
			string cMovieTitle = movieTitle;
			string cDate_time = date_time;
			string cSeats = seats;
			string cName = name;
			string cEmail = email;

			return Tuple.Create(cMovieTitle, cDate_time, cSeats, cName, cEmail);
		}
	}
}
