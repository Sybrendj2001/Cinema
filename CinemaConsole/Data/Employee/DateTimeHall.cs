using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.BackEnd;
using CinemaConsole.Data.Employee;

namespace CinemaConsole.Data
{
	public class DateTimeHall
	{

		private AdminData AD = new AdminData();

		public DateTimeHall(DateTime DT, int HallInput, string MovieName)
		{
			string datetime = DT.ToString("yyyy") + "-" + DT.ToString("MM") + "-" + DT.ToString("dd") + " " + DT.ToString("HH") + ":" + DT.ToString("mm");

			DateTime dt = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

			AD.CreateDateTime(dt, AD.GetMovieID(MovieName), HallInput);

			int dateID = AD.GetDateID(dt, HallInput);
			TheatherHalls hall = new TheatherHalls(HallInput, dateID);
		}
	}

	public class TheatherHalls
	{
		AdminData AD = new AdminData();

		public TheatherHalls(int hallNumber, int dateID)
		{


			if (hallNumber == 1)
			{
				AD.CreateHall(150, 14, 12, dateID);
				int hallID = AD.GetHallID(dateID);
				Hall1(hallID);
			}
			else if (hallNumber == 2)
			{
				AD.CreateHall(300, 19, 18, dateID);
				int hallID = AD.GetHallID(dateID);
				Hall2(hallID);
			}
			else if (hallNumber == 3)
			{
				AD.CreateHall(500, 20, 30, dateID);
				int hallID = AD.GetHallID(dateID);
				Hall3(hallID);
			}
		}

		private void Hall1(int hallID)
		{

			string SeatName = "";
			bool SeatAvail = false;

			for (int i = 0; i < 14; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					SeatName = "(row " + (14 - i) + " seat ";
					SeatAvail = true;
					if ((i == 0 || i > 11) && (j > 1 && j < 10))
					{
						SeatName += (j - 1) + ")";
					}
					else if (i > 2 && i < 11)
					{
						SeatName += (j + 1) + ")";
					}
					else if ((i == 1 || i == 2 || i == 11) && (j > 0 && j < 11))
					{
						SeatName += (j) + ")";
					}
					else
					{
						SeatName = "(No Seat)";
						SeatAvail = false;
					}

					AD.CreateSeat(10.00, i, j, hallID, SeatAvail, SeatName);
				}
			}
		}

		private void Hall2(int hallID)
		{
			string SeatName = "";
			bool SeatAvail = false;

			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < 18; j++)
				{
					SeatName = "(row " + (19 - i) + " seat ";
					SeatAvail = true;

					if ((i == 18 || i == 17) && (j > 2 && j < 15))
					{
						SeatName += (j - 2) + ")";
					}
					else if ((i < 17 && i > 13) && (j > 1 && j < 16))
					{
						SeatName += (j - 1) + ")";
					}
					else if ((i < 6 || (i > 10 && i < 14)) && (j > 0 && j < 17))
					{
						SeatName += (j) + ")";
					}
					else if (i > 5 && i < 11)
					{
						SeatName += (j + 1) + ")";
					}
					else
					{
						SeatName = "(No Seat)";
						SeatAvail = false;
					}
					AD.CreateSeat(10.00, i, j, hallID, SeatAvail, SeatName);
				}
			}

		}

		private void Hall3(int hallID)
		{
			string SeatName = "";
			bool SeatAvail = false;

			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 30; j++)
				{
					SeatName = "(row " + (20 - i) + " seat ";
					SeatAvail = true;

					if (i == 19 && (j > 7 && j < 22))
					{
						SeatName += (j - 7) + ")";
					}
					else if (i == 18 && (j > 6 && j < 23))
					{
						SeatName += (j - 6) + ")";
					}
					else if (i == 17 && (j > 4 && j < 25))
					{
						SeatName += (j - 4) + ")";
					}
					else if (i == 0 && (j > 3 && j < 26))
					{
						SeatName += (j - 3) + ")";
					}
					else if ((i == 16 || i == 15 || (i < 5 && i > 0)) && (j > 2 && j < 27))
					{
						SeatName += (j - 2) + ")";
					}
					else if ((i == 14 || i == 13 || i == 5) && (j > 1 && j < 28))
					{
						SeatName += (j - 1) + ")";
					}
					else if ((i == 12 || i == 6) && (j > 0 && j < 29))
					{
						SeatName += (j) + ")";
					}
					else if (i > 6 && i < 12)
					{
						SeatName += (j + 1) + ")";
					}
					else
					{
						SeatName = "(No Seat)";
						SeatAvail = false;
					}
					AD.CreateSeat(10.00, i, j, hallID, SeatAvail, SeatName);
				}
			}
		}
	}
}
