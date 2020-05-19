using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using System.Data;

namespace CinemaConsole.Data.BackEnd
{
	public class ChangeData : Connecter
	{
		public void CreateProfile(int totalprofiles, string username, string password, string function)
		{
			try
			{
				Connection.Open();

				string stringToInsert = @"INSERT INTO login (ID, Username, Password, Functions) VALUES (@ID, @Username, @Password, @Functions)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter IDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
				MySqlParameter UsernameParam = new MySqlParameter("@Username", MySqlDbType.VarChar);
				MySqlParameter PasswordParam = new MySqlParameter("@Password", MySqlDbType.VarChar);
				MySqlParameter FunctionParam = new MySqlParameter("@Functions", MySqlDbType.VarChar);

				IDParam.Value = totalprofiles;
				UsernameParam.Value = username;
				PasswordParam.Value = password;
				FunctionParam.Value = function;

				command.Parameters.Add(IDParam);
				command.Parameters.Add(UsernameParam);
				command.Parameters.Add(PasswordParam);
				command.Parameters.Add(FunctionParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

		public void UpdateMovie(int id, string name = "", int year = -1, int minimumage = -1, string summary = "", string actors = "")
		{
			try
			{
				Connection.Open();
				bool updating = true;
				while (updating)
				{

					MySqlParameter ParamID = new MySqlParameter("@MovieID", MySqlDbType.Int32);
					ParamID.Value = id;


					//Check which variables you need to get
					if (name != "")
					{
						string UpdateName = @"UPDATE movie SET MovieName = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandName = new MySqlCommand(UpdateName, Connection);
						MySqlParameter NameParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);
						
						NameParam.Value = name;

						commandName.Parameters.Add(ParamID);
						commandName.Parameters.Add(NameParam);

						commandName.Prepare();
						commandName.ExecuteNonQuery();

						name = "";
					}
					else if (year != -1)
					{
						string UpdateYear = @"UPDATE movie SET MovieYear = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandYear = new MySqlCommand(UpdateYear, Connection);
						MySqlParameter YearParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						YearParam.Value = year;

						commandYear.Parameters.Add(ParamID);
						commandYear.Parameters.Add(YearParam);

						commandYear.Prepare();
						commandYear.ExecuteNonQuery();

						year = -1;
					}
					else if (minimumage != -1)
					{
						string UpdateAge = @"UPDATE movie SET MovieMinimumAge = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandAge = new MySqlCommand(UpdateAge, Connection);
						MySqlParameter AgeParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						AgeParam.Value = minimumage;

						commandAge.Parameters.Add(ParamID);
						commandAge.Parameters.Add(AgeParam);

						commandAge.Prepare();
						commandAge.ExecuteNonQuery();

						minimumage = -1;
					}
					else if (summary != "")
					{
						string UpdateSum = @"UPDATE movie SET MovieSummary = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandSum = new MySqlCommand(UpdateSum, Connection);
						MySqlParameter SumParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						SumParam.Value = summary;

						commandSum.Parameters.Add(ParamID);
						commandSum.Parameters.Add(SumParam);

						commandSum.Prepare();
						commandSum.ExecuteNonQuery();

						summary = "";
					}
					else if (actors != "")
					{
						string UpdateActors = @"UPDATE movie SET MovieActors = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandActor = new MySqlCommand(UpdateActors, Connection);
						MySqlParameter ActorsParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						ActorsParam.Value = actors;

						commandActor.Parameters.Add(ParamID);
						commandActor.Parameters.Add(ActorsParam);

						commandActor.Prepare();
						commandActor.ExecuteNonQuery();

						actors = "";
					}
					else
					{
						updating = false;
						break;
					}
				}
			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}
		}

		public bool CheckTicket(string ticketid)
		{
			bool TicketExists = false;
			try
			{
				Connection.Open();
				string stringToCheck = @"SELECT TicketCode FROM ticket WHERE TicketCode = @TicketID";

				MySqlCommand command = new MySqlCommand(stringToCheck, Connection);
				MySqlParameter ParamticketID = new MySqlParameter("@TicketID", MySqlDbType.VarChar);

                ParamticketID.Value = ticketid;
                command.Parameters.Add(ParamticketID);

                MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					int TicketCheck = dataReader.GetInt32("COUNT(*)");
					if (TicketCheck == 1)
					{
						TicketExists = true;
						dataReader.Close();
					}
					else
					{
						TicketExists = false;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}
			return TicketExists;
		}

		//Only used when a display function is called individueally.
		public void DisplayProducts()
		{
			Console.Clear();
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Restaurant menu:");
			try
			{
				Connection.Open();
				string stringToDisplay = @"SELECT * FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();
				while (dataReader.Read())
				{
					double test = dataReader.GetDouble("Price");
					Console.WriteLine("(" + dataReader["ItemID"] + ") " + dataReader["ItemName"] + "    €" + test.ToString("0.00"));
				}

				dataReader.Close();
			}
			catch (MySqlException ex)
			{

				throw;
			}
			finally
			{
				Connection.Close();
			}
		}

		//Called from within another function. Lacks any connection.open() and connection.Close() functions.
		public void DisplayProduct()
		{
			Console.Clear();
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Restaurant menu:");
			try
			{
				string stringToDisplay = @"SELECT * FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();
				while (dataReader.Read())
				{
					double test = dataReader.GetDouble("Price");
					Console.WriteLine("(" + dataReader["ItemID"] + ") " + dataReader["ItemName"] + "    €" + test.ToString("0.00"));
				}

				dataReader.Close();
			}
			catch (MySqlException ex)
			{

				throw;
			}
		}

		public void CreateProduct(string itemname, double price)
		{
			try
			{
				Connection.Open();

				string stringToInsert = @"INSERT INTO restaurantitems (ItemName, Price) VALUES (@ItemName, @Price)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter ItemNameParam = new MySqlParameter("@ItemName", MySqlDbType.VarChar);
				MySqlParameter PriceParam = new MySqlParameter("@Price", MySqlDbType.Double);

				ItemNameParam.Value = itemname;
				PriceParam.Value = price;

				command.Parameters.Add(ItemNameParam);
				command.Parameters.Add(PriceParam);

				command.Prepare();
				command.ExecuteNonQuery();

				DisplayProduct();
			}
			catch (MySqlException ex)
			{

				throw;
			}
			finally
			{
				Connection.Close();
			}
		}

		public void UpdateProduct(int id = -1, string name = "", double price = -1)
		{
			try
			{
				Connection.Open();
				if (name != "" && price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName, Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = id;
					ParamNewName.Value = name;
					ParamNewPrice.Value = price;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamNewPrice);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();

					DisplayProduct();
				}

				else if (name != "" && price == -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);

					ParamID.Value = id;
					ParamNewName.Value = name;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();

					DisplayProduct();
				}

				else if (name == "" && price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = id;
					ParamNewPrice.Value = price;

					command.Parameters.Add(ParamNewPrice);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();

					DisplayProduct();
				}
			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}	
        }

        public void DeleteProduct(int deleteItem)
		{
			try
			{
				Connection.Open();

				string stringToDelete = "DELETE FROM restaurantitems WHERE ItemID = @ItemID";

				MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
				MySqlParameter ItemIDParam = new MySqlParameter("@ItemID", MySqlDbType.Int32);

				ItemIDParam.Value = deleteItem;

				command.Parameters.Add(ItemIDParam);

				command.Prepare();
				command.ExecuteNonQuery();

				DisplayProduct();

			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}
		}

		public string checkLoginAndFunction(string username, string password)
		{
			//if string is empty it means you are not logged in, otherwise you are
			string function = "";
			try
			{
				Connection.Open();
				string stringToCheck = @"SELECT * FROM login WHERE username = @uname AND password = @pword";

				MySqlCommand command = new MySqlCommand(stringToCheck, Connection);
				MySqlParameter ParamUsername = new MySqlParameter("@uname", MySqlDbType.VarChar);
				MySqlParameter ParamPassword = new MySqlParameter("@pword", MySqlDbType.VarChar);

				ParamUsername.Value = username;
				ParamPassword.Value = password;

				command.Parameters.Add(ParamUsername);
				command.Parameters.Add(ParamPassword);

				MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					function = dataReader.GetString("Functions");
				}
				dataReader.Close();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				Connection.Close();
			}
			return function;
		}

		public void InsertMovie(string name, int year, int mage, string msummary, string actors, int duration, string genre)
		{
			try
			{
				Connection.Open();
				string stringToInsert = "INSERT INTO movie (MovieName, MovieYear, MovieMinimumAge, MovieSummary, MovieActors, MovieDuration, MovieGenre) " +
					"VALUES (@Name, @Year, @MAge, @MSummary, @MovieActors, @MovieDuration, @MovieGenre)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter NameParam = new MySqlParameter("@Name", MySqlDbType.VarChar);
				MySqlParameter YearParam = new MySqlParameter("@Year", MySqlDbType.Int32);
				MySqlParameter MAgeParam = new MySqlParameter("@MAge", MySqlDbType.Int32);
				MySqlParameter MSummaryParam = new MySqlParameter("@MSummary", MySqlDbType.LongText);
				MySqlParameter ActorsParam = new MySqlParameter("@MovieActors", MySqlDbType.LongText);
				MySqlParameter DurationParam = new MySqlParameter("@MovieDuration", MySqlDbType.Int32);
				MySqlParameter GenreParam = new MySqlParameter("@MovieGenre", MySqlDbType.VarChar);

				NameParam.Value = name;
				YearParam.Value = year;
				MAgeParam.Value = mage;
				MSummaryParam.Value = msummary;
				ActorsParam.Value = actors;
				DurationParam.Value = duration;
				GenreParam.Value = genre;

				command.Parameters.Add(NameParam);
				command.Parameters.Add(YearParam);
				command.Parameters.Add(MAgeParam);
				command.Parameters.Add(MSummaryParam);
				command.Parameters.Add(ActorsParam);
				command.Parameters.Add(DurationParam);
				command.Parameters.Add(GenreParam);

				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (Exception)
			{

                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void ReserveTicket(string Owner, string Email, string TicketCode, int MovieID, int Amount, int seatX, int seatY, int DateID, int Hall, double TotalPrice, int HallID)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO ticket (Owner, Email, TicketCode, MovieID, Amount, seatX, seatY, DateID, HallID, TotalPrice, Hall) VALUES (@Owner, @Email, @TicketCode, @MovieID, @Amount, @seatX, @seatY, @DateID, @HallID, @TotalPrice, @Hall)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter OwnerParam = new MySqlParameter("@Owner", MySqlDbType.VarChar);
                MySqlParameter EmailParam = new MySqlParameter("@Email", MySqlDbType.VarChar);
                MySqlParameter TicketCodeParam = new MySqlParameter("@TicketCode", MySqlDbType.VarChar);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.Int32);
                MySqlParameter AmountParam = new MySqlParameter("@Amount", MySqlDbType.Int32);
                MySqlParameter seatXParam = new MySqlParameter("@seatX", MySqlDbType.Int32);
                MySqlParameter seatYParam = new MySqlParameter("@seatY", MySqlDbType.Int32);
                MySqlParameter DateIDParam = new MySqlParameter("@DateID", MySqlDbType.Int32);
                MySqlParameter HallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);
                MySqlParameter TotalPriceParam = new MySqlParameter("@TotalPrice", MySqlDbType.Double);
				MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);


				//TicketIDParam.Value = TicketID;
				OwnerParam.Value = Owner;
                EmailParam.Value = Email;
                TicketCodeParam.Value = TicketCode;
                MovieIDParam.Value = MovieID;
                AmountParam.Value = Amount;
                seatXParam.Value = seatX;
                seatYParam.Value = seatY;
                DateIDParam.Value = DateID;
                HallParam.Value = Hall;
				HallIDParam.Value = HallID;
				TotalPriceParam.Value = TotalPrice;

                //command.Parameters.Add(TicketIDParam);
                command.Parameters.Add(OwnerParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(TicketCodeParam);
                command.Parameters.Add(MovieIDParam);
                command.Parameters.Add(AmountParam);
                command.Parameters.Add(seatXParam);
                command.Parameters.Add(seatYParam);
                command.Parameters.Add(DateIDParam);
                command.Parameters.Add(HallIDParam);
                command.Parameters.Add(TotalPriceParam);
				command.Parameters.Add(HallParam);

				command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DeleteReservation(string ticketcode)
        {
            try
            {
				AdminData AD = new AdminData();
				int seatX = 0;
				int seatY = 0;
				int hallID;
				int amount;

				Connection.Open();

                string stringToDelete = @"DELETE FROM ticket WHERE TicketCode = @TicketCode";
                string TicketInfo = @"SELECT * FROM ticket";

                MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
                MySqlParameter TicketCodeParam = new MySqlParameter("@TicketCode", MySqlDbType.String);
                MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);

                using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getTicketInfo);
                    string TicketCode;
                    string TicketID;
                    string MovieID;
                    string DateID;
					int dateid;
					bool isFound = false;
					double TotalPrice;

					while (true)
					{
						foreach (DataRow row in dataTable.Rows)
						{
							TicketCode = row["TicketCode"].ToString();
							TicketID = row["TicketID"].ToString();
							MovieID = row["MovieID"].ToString();
							DateID = row["DateID"].ToString();
							hallID = Convert.ToInt32(row["HallID"]);
							amount = Convert.ToInt32(row["amount"]);
							seatX = Convert.ToInt32(row["seatX"]);
							seatY = Convert.ToInt32(row["seatY"]);
							dateid = Convert.ToInt32(row["DateID"]);
							TotalPrice = Convert.ToDouble(row["TotalPrice"]);
							double pricedelete = -TotalPrice;

							if (TicketCode == ticketcode)
							{
								ShowData DeleteTicket = new ShowData();
								// Ticket and contact information overview to check if you want to remove the right ticket.
								DeleteTicket.Overview(TicketID, MovieID, DateID);
								isFound = true;

								Console.WriteLine("\nDo you really want to remove this reservation?\n[1] Remove reservation\n[2] Cancel");
								string CancelOrDelete = Console.ReadLine();

								if (CancelOrDelete == "1")
								{
									TicketCodeParam.Value = ticketcode;
									command.Parameters.Add(TicketCodeParam);
									command.Prepare();
									command.ExecuteNonQuery();
									
									DateTime MonthYear = AD.GetDate(dateid);
									Connection.Close();
									var MonthMM = Convert.ToDateTime(MonthYear).ToString("MM");
									int Month = Convert.ToInt32(MonthMM);
									var Yearyyyy = Convert.ToDateTime(MonthYear).ToString("yyyy");
									int Year = Convert.ToInt32(Yearyyyy);

									AD.UpdateRevenueYear(Year, pricedelete);
									AD.UpdateRevenueMonth(Month, Year, pricedelete);

									// This set the seats back to available
									AD.switchAvail((seatX - 1), (seatY - 1), hallID, amount, true);
									Console.WriteLine("\nReservation removed. Press enter to go back to the menu");
									Console.ReadLine();
									Console.Clear();
									break;
								}

								else if (CancelOrDelete == "2")
								{
									Console.Clear();
									break;
								}
								break;
							}
						}

						if (isFound)
						{
							Console.Clear();
							break;
						}

						else
						{
							Console.Clear();
							Console.WriteLine("\nThere were no results found with ticketnumber: " + ticketcode + "\nPress enter to go back to the menu");
							Console.ReadLine();
							Console.Clear();
							break;
						}
					}
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

		public void ReservationAmount()
		{
			try
			{
				Connection.Open();
				int amount = 0;

				string AddToTicketAmount = @"SELECT * FROM ticket";				

				MySqlCommand command = new MySqlCommand(AddToTicketAmount, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();
				while (dataReader.Read())
				{
					amount += dataReader.GetInt32("amount");
				}

				dataReader.Close();

				Console.Write($"\nThere are currently reservations for ");

				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write($"{amount}");
				Console.ResetColor();

				Console.Write(" people.\n");

				Console.WriteLine("Press [enter] to continue.");
				Console.ReadLine();
				Console.Clear();
			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}
		}

		public bool checkIfPExists(int ItemID)
		{
			List<int> ProductIDs = new List<int>();
			ProductIDs.Clear();

			try
			{
				Connection.Open();
				string IntToCheck = @"SELECT * FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(IntToCheck, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();
				while (dataReader.Read())
				{
					int test = dataReader.GetInt32("ItemID");
					ProductIDs.Add(test);
				}
				dataReader.Close();

				if (ProductIDs.Contains(ItemID) == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
			}

			//door ID column lopen zoals bij reservation amount en dan elke item in een list gooien.
			//Daarna checken of de ID in de list zit. Zo ja, dan gaat ie in restaurant door naar edit. Zo nee geeft ie een error.
		}
    }
}