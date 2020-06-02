using System;
using System.Globalization;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Data
{
	public class DateTimeHall
	{
		/// <summary>Data retraction</summary>
		private readonly AdminData AD = new AdminData();

		/// <summary>
		/// Create a new hall
		/// </summary>
		/// <param name="DT">The time that hall is needed</param>
		/// <param name="HallInput">The hall that is needed</param>
		/// <param name="MovieName">The hall the movie is played in</param>
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
		/// <summary>Database</summary>
		AdminData AD = new AdminData();

		/// <summary>prices for the seats for all the different rings</summary>
		private Tuple<double, double, double> prices { get; set; }

		/// <summary>
		/// A hall
		/// </summary>
		/// <param name="HallNumber">Number of the hall</param>
		/// <param name="DateID">Date that the hall is needed</param>
		public TheatherHalls(int HallNumber, int DateID)
		{

			prices = AD.getPrices(HallNumber);

			if (HallNumber == 1)
			{
				AD.CreateHall(150, 14, 12, DateID,prices.Item1,prices.Item2,prices.Item3);
				int hallID = AD.GetHallID(DateID);
				Hall1(hallID);
			}
			else if (HallNumber == 2)
			{
				AD.CreateHall(300, 19, 18, DateID, prices.Item1, prices.Item2, prices.Item3);
				int hallID = AD.GetHallID(DateID);
				Hall2(hallID);
			}
			else if (HallNumber == 3)
			{
				AD.CreateHall(500, 20, 30, DateID, prices.Item1, prices.Item2, prices.Item3);
				int hallID = AD.GetHallID(DateID);
				Hall3(hallID);
			}
		}

		/// <summary>
		/// Create hall 1
		/// </summary>
		/// <param name="HallID">Used connect to a date</param>
		private void Hall1(int HallID)
		{
			string SeatName = "";
			bool SeatAvail = false;
			double price = 10.00;

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

					//Give the right prices to the rigth seats
					if((j == 5 || j == 6)&& (i > 4 && i < 9))
					{
						price = prices.Item1;
					}
					else if ((j == 5 || j == 6) && (i > 2 && i < 11))
					{
						price = prices.Item2;
					}
					else if ((j == 4 || j == 7) && (i > 3 && i < 10))
					{
						price = prices.Item2;
					}
					else if ((j == 3 || j == 8) && (i > 4 && i < 9))
					{
						price = prices.Item2;
					}
					else
					{
						price = prices.Item3;
					}
					AD.CreateSeat(price, i, j, HallID, SeatAvail, SeatName);
				}
			}
		}

		/// <summary>
		/// Create hall 2
		/// </summary>
		/// <param name="HallID">Used connect to a date</param>
		private void Hall2(int HallID)
		{
			string SeatName = "";
			bool SeatAvail = false;
			double price = 0.0;

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
					price = Price2(i,j);
					AD.CreateSeat(price, i, j, HallID, SeatAvail, SeatName);
				}
			}

		}

		/// <summary>
		/// Create hall 3
		/// </summary>
		/// <param name="HallID">Used connect to a date</param>
		private void Hall3(int HallID)
		{
			string SeatName = "";
			bool SeatAvail = false;
			double price = 0.0;

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
					price = Price3(i, j);
					
					AD.CreateSeat(price, i, j, HallID, SeatAvail, SeatName);
				}
			}
		}

		/// <summary>
		/// Create price for the middle circle
		/// </summary>
		/// <param name="i">Diameter for the circle</param>
		/// <param name="j">Diameter for the circle</param>
		/// <returns>The seats that need their price changed</returns>
		private double Price2(int i, int j)
		{
			double price = 0.0;
			if ((j == 8 || j == 9) && (i> 4 && i < 13))
			{
				price = prices.Item1;
			}
			else if ((j == 7 || j == 10) && (i > 5 && i < 12))
			{
				price = prices.Item1;
			}
			else if ((j == 6 || j == 11) && (i > 6 && i < 11))
			{
				price = prices.Item1;
			}
			else if ((j > 5 && j < 12) && (i > 0 && i < 16))
			{
				price = prices.Item2;
			}
			else if ((j == 5 || j == 12) && (i > 1 && i < 14))
			{
				price = prices.Item2;
			}
			else if ((j == 4 || j == 13) && (i > 3 && i < 13))
			{
				price = prices.Item2;
			}
			else if ((j == 3 || j == 14) && (i > 5 && i < 12))
			{
				price = prices.Item2;
			}
			else if ((j == 2 || j == 15) && (i > 7 && i < 11))
			{
				price = prices.Item2;
			}
			else
			{
				price = prices.Item3;
			}
			return price;
		}

		/// <summary>
		/// Create price for the inner circle
		/// </summary>
		/// <param name="i">Diameter for the circle</param>
		/// <param name="j">Diameter for the circle</param>
		/// <returns>The seats that need their price changed</returns>
		private double Price3(int i, int j)
		{
			double price = 0.0;

			if ((j > 12 && j < 17)&&(i > 3 && i < 13))
			{
				price = prices.Item1;
			}
			else if ((j == 12 || j == 17) && (i > 4 && i < 12))
			{
				price = prices.Item1;
			}
			else if ((j == 11 || j == 18) && (i > 5 && i < 12))
			{
				price = prices.Item1;
			}
			else if ((j > 11 && j < 18) && (i > 0 && i < 17))
			{
				price = prices.Item2;
			}
			else if ((j == 10 || j == 11 || j == 18 || j == 19) && (i > 0 && i < 16))
			{
				price = prices.Item2;
			}
			else if ((j == 9 || j == 20) && (i > 0 && i < 15))
			{
				price = prices.Item2;
			}
			else if ((j == 8 || j == 21) && (i > 1 && i < 14))
			{
				price = prices.Item2;
			}
			else if ((j == 7 || j == 22) && (i > 3 && i < 12))
			{
				price = prices.Item2;
			}
			else if ((j == 6 || j == 23) && (i > 5 && i < 11))
			{
				price = prices.Item2;
			}
			else if ((j == 5 || j == 24) && (i > 7 && i < 10))
			{
				price = prices.Item2;
			}
			else
			{
				price = prices.Item3;
			}

			return price;
		}
	}
}
