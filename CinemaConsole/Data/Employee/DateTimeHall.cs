using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data.Employee;

namespace CinemaConsole.Data
{
    public class DateTimeHall
    {
		private int id { get; set; }
		private string date { get; set; }

		private string time { get; set; }

		private TheatherHalls hall { get; set; }

		public DateTimeHall(string DateInput, string TimeInput, int HallInput, Movies movie)
		{
			date = DateInput;
			time = TimeInput;
			hall = new TheatherHalls(HallInput);
			id = HallID(movie);
		}

		public Tuple<int, string, string, TheatherHalls> getDateInfo()
		{
			return Tuple.Create(id, date, time, hall);
		}
		/// <summary>
		/// Creating a new unique ID and checking for missing ID's
		/// </summary>
		private int HallID(Movies movie)
		{
			int idd;
			for (int i = 0; i < movie.DateTimeHallsList.Count; i++)
			{
				idd = i + 1;
				if (movie.DateTimeHallsList[i].getDateInfo().Item1 != idd)
				{
					return idd;
				}
			}
			return movie.DateTimeHallsList.Count + 1;
		}
	}

	public class TheatherHalls
	{
		private Seat[][] hall { get; set; }
		private int HallNumber { get; set; }

		public TheatherHalls(int hallNumber)
		{
			HallNumber = hallNumber;

			if (hallNumber == 1)
			{
				hall = Hall1();
			}
			else if (hallNumber == 2)
			{
				hall = Hall2();
			}
			else if (hallNumber == 3)
			{
				hall = Hall3();
			}
		}

		private Seat[][] Hall1()
		{
			Seat[][] hall = new Seat[14][];

			for (int i = 0; i < 14; i++)
			{
				hall[i] = new Seat[12];
			}

			string SeatName = "";
			string SeatPlacement = "";
			bool SeatAvail = false;

			for (int i = 0; i < 14; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					SeatName = "(row " + (14 - i) + " seat ";
					SeatAvail =	true;
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

					SeatPlacement = "(" + j + "," + i + ")";
					hall[i][j] = new Seat(SeatName, SeatPlacement, SeatAvail);
				}
			}

			return hall;
		}

		private Seat[][] Hall2()
		{
			Seat[][] hall = new Seat[19][];

			for (int i = 0; i < hall.Length; i++)
			{
				hall[i] = new Seat[18];
			}

			string SeatName = "";
			string SeatPlacement = "";
			bool SeatAvail = false;

			for (int i = 0; i < hall.Length; i++)
			{
				for(int j = 0; j < hall[i].Length; j++)
				{
					SeatName = "(row " + (19 - i) + " seat ";
					SeatPlacement = "(" + j + "," + i + ")";
					SeatAvail = true;

					if ((i == 18 || i == 17) && (j > 2 && j < 15))
					{
						SeatName += (j-2) + ")";
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
					
					SeatPlacement = "(" + j + "," + i + ")";
					hall[i][j] = new Seat(SeatName, SeatPlacement, SeatAvail);
				}
			}
			return hall;
		}

		private Seat[][] Hall3()
		{
			Seat[][] hall = new Seat[20][];

			for (int i = 0; i < hall.Length; i++)
			{
				hall[i] = new Seat[30];
			}

			string SeatName = "";
			string SeatPlacement = "";
			bool SeatAvail = false;

			for (int i = 0; i < hall.Length; i++)
			{
				for (int j = 0; j < hall[i].Length; j++)
				{
					SeatName = "(row " + (20 - i) + " seat ";
					SeatPlacement = "(" + j + "," + i + ")";
					SeatAvail = true;

					if (i == 19 && (j > 7 && j < 22))
					{
						SeatName += (j-7) + ")";
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
					
					SeatPlacement = "(" + j + "," + i + ")";
					hall[i][j] = new Seat(SeatName, SeatPlacement, SeatAvail);
				}
			}
			return hall;
		}

		public  Tuple<Seat[][], int> getHallInfo()
		{
			return Tuple.Create(hall,HallNumber);
		}
	}
}
