using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace CinemaConsole.Data
{
	public class ChangeData : Connecter
	{
		/// <summary>
		/// Update a movie
		/// </summary>
		/// <param name="ID">The ID of the movie</param>
		/// <param name="Name">The new name of the movie</param>
		/// <param name="Year">The new year of the movie that played in</param>
		/// <param name="MinimumAge">The minimum age that is required for the movie</param>
		/// <param name="Summary">The summary for the movie</param>
		/// <param name="Actors">The actors for the movie</param>
		public void UpdateMovie(int ID, string Name = "", int Year = -1, int MinimumAge = -1, string Summary = "", string Actors = "")
		{
			try
			{
				Connection.Open();
				bool updating = true;
				while (updating)
				{

					MySqlParameter ParamID = new MySqlParameter("@MovieID", MySqlDbType.Int32);
					ParamID.Value = ID;


					//Check which variables you need to get
					if (Name != "")
					{
						string UpdateName = @"UPDATE movie SET MovieName = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandName = new MySqlCommand(UpdateName, Connection);
						MySqlParameter NameParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);
						
						NameParam.Value = Name;

						commandName.Parameters.Add(ParamID);
						commandName.Parameters.Add(NameParam);

						commandName.Prepare();
						commandName.ExecuteNonQuery();

						Name = "";
					}
					else if (Year != -1)
					{
						string UpdateYear = @"UPDATE movie SET MovieYear = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandYear = new MySqlCommand(UpdateYear, Connection);
						MySqlParameter YearParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						YearParam.Value = Year;

						commandYear.Parameters.Add(ParamID);
						commandYear.Parameters.Add(YearParam);

						commandYear.Prepare();
						commandYear.ExecuteNonQuery();

						Year = -1;
					}
					else if (MinimumAge != -1)
					{
						string UpdateAge = @"UPDATE movie SET MovieMinimumAge = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandAge = new MySqlCommand(UpdateAge, Connection);
						MySqlParameter AgeParam = new MySqlParameter("@NewType", MySqlDbType.Int32);

						AgeParam.Value = MinimumAge;

						commandAge.Parameters.Add(ParamID);
						commandAge.Parameters.Add(AgeParam);

						commandAge.Prepare();
						commandAge.ExecuteNonQuery();

						MinimumAge = -1;
					}
					else if (Summary != "")
					{
						string UpdateSum = @"UPDATE movie SET MovieSummary = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandSum = new MySqlCommand(UpdateSum, Connection);
						MySqlParameter SumParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						SumParam.Value = Summary;

						commandSum.Parameters.Add(ParamID);
						commandSum.Parameters.Add(SumParam);

						commandSum.Prepare();
						commandSum.ExecuteNonQuery();

						Summary = "";
					}
					else if (Actors != "")
					{
						string UpdateActors = @"UPDATE movie SET MovieActors = @NewType WHERE MovieID = @MovieID";

						MySqlCommand commandActor = new MySqlCommand(UpdateActors, Connection);
						MySqlParameter ActorsParam = new MySqlParameter("@NewType", MySqlDbType.VarChar);

						ActorsParam.Value = Actors;

						commandActor.Parameters.Add(ParamID);
						commandActor.Parameters.Add(ActorsParam);

						commandActor.Prepare();
						commandActor.ExecuteNonQuery();

						Actors = "";
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

		/// <summary>
		/// Displays the restaurant products
		/// </summary>
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
					Console.WriteLine("(" + dataReader["ItemID"] + ") " + dataReader["ItemName"] + "    €" + test.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
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

		/// <summary>
		/// Shows all products in the restaurant
		/// </summary>
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

		/// <summary>
		/// Show an item of a product (name or price)
		/// </summary>
		/// <param name="ProductID">id of the product</param>
		/// <param name="Option">select 1 = name, 2 = price</param>
		public void ShowProductItem(int ProductID, int Option)
        {
			try
            {
				Connection.Open();
				string stringToDisplay = @"SELECT * FROM restaurantitems WHERE ItemID = @ItemID";
				MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);
				MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
				command.Parameters.AddWithValue("@ItemID", ProductID);

				using (MySqlDataReader getProductInfo = command.ExecuteReader())
				{
					while (getProductInfo.Read())
					{
						switch (Option)
						{
							case 1:
								Console.WriteLine("\nCurrent item name: " + getProductInfo["ItemName"].ToString());
								break;
							case 2:
								Console.WriteLine("\nCurrent price: " + getProductInfo["Price"].ToString());
								break;
						}
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

		/// <summary>
		/// Create a new product
		/// </summary>
		/// <param name="ItemName">Product name</param>
		/// <param name="Price">Product price</param>
		public void CreateProduct(string ItemName, double Price)
		{
			try
			{
				Connection.Open();

				string stringToInsert = @"INSERT INTO restaurantitems (ItemName, Price) VALUES (@ItemName, @Price)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter ItemNameParam = new MySqlParameter("@ItemName", MySqlDbType.VarChar);
				MySqlParameter PriceParam = new MySqlParameter("@Price", MySqlDbType.Double);

				ItemNameParam.Value = ItemName;
				PriceParam.Value = Price;

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

		/// <summary>
		/// Update a product name and price
		/// </summary>
		/// <param name="ID">The ID of the product you want to update</param>
		/// <param name="Name">The new name of the product</param>
		/// <param name="Price">The new price of the product</param>
		public void UpdateProduct(int ID = -1, string Name = "", double Price = -1)
		{
			try
			{
				Connection.Open();
				if (Name != "" && Price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName, Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = ID;
					ParamNewName.Value = Name;
					ParamNewPrice.Value = Price;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamNewPrice);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();

					DisplayProduct();
				}

				else if (Name != "" && Price == -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET ItemName = @NewName WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewName = new MySqlParameter("@NewName", MySqlDbType.VarChar);

					ParamID.Value = ID;
					ParamNewName.Value = Name;

					command.Parameters.Add(ParamNewName);
					command.Parameters.Add(ParamID);

					command.Prepare();
					command.ExecuteNonQuery();

					DisplayProduct();
				}

				else if (Name == "" && Price != -1)
				{
					string stringToUpdate = @"UPDATE restaurantitems SET Price = @NewPrice WHERE ItemID = @ItemID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);
					MySqlParameter ParamNewPrice = new MySqlParameter("@NewPrice", MySqlDbType.Double);

					ParamID.Value = ID;
					ParamNewPrice.Value = Price;

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

		/// <summary>
		/// Delete a product of the restaurant
		/// </summary>
		/// <param name="DeleteItemID">The ID item to delete</param>
        public void DeleteProduct(int DeleteItemID)
		{
			try
			{
				Connection.Open();

				string stringToDelete = "DELETE FROM restaurantitems WHERE ItemID = @ItemID";

				MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
				MySqlParameter ItemIDParam = new MySqlParameter("@ItemID", MySqlDbType.Int32);

				ItemIDParam.Value = DeleteItemID;

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

		/// <summary>
		/// Checks if you can login
		/// </summary>
		/// <param name="Username">Username for the login</param>
		/// <param name="Password">Password for the login</param>
		/// <returns>The function if you get logged in</returns>
		public string checkLoginAndFunction(string Username, string Password)
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

				ParamUsername.Value = Username;
				ParamPassword.Value = Password;

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

		/// <summary>
		/// Create a new movie
		/// </summary>
		/// <param name="Name">Name of the movie</param>
		/// <param name="Year">Year the movie was made in</param>
		/// <param name="MinimumAge">The minimum age for the movie to watch it</param>
		/// <param name="Summary">The summary of movie in short</param>
		/// <param name="Actors">Actors that act in the movie</param>
		/// <param name="Duration">The duration of the movie</param>
		/// <param name="Genre">The genre of the movie</param>
		public void InsertMovie(string Name, int Year, int MinimumAge, string Summary, string Actors, int Duration, string Genre)
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

				NameParam.Value = Name;
				YearParam.Value = Year;
				MAgeParam.Value = MinimumAge;
				MSummaryParam.Value = Summary;
				ActorsParam.Value = Actors;
				DurationParam.Value = Duration;
				GenreParam.Value = Genre;

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

		/// <summary>
		/// Reserve a ticket
		/// </summary>
		/// <param name="Owner">Name of the owner of the ticket</param>
		/// <param name="Email">Mail of the owener of the ticket</param>
		/// <param name="TicketCode">The code for the ticket</param>
		/// <param name="MovieID">The ID of the movie</param>
		/// <param name="Amount">The amount of seats reserved</param>
		/// <param name="SeatX">The X value the reservation starts</param>
		/// <param name="SeatY">The Y value the reservation starts</param>
		/// <param name="DateID">The ID of the date the movie is played</param>
		/// <param name="Hall">The hall the movie is played in</param>
		/// <param name="TotalPrice">The total price for all the tickets</param>
		/// <param name="HallID">The ID of the hall the movie is played in</param>
		public void ReserveTicket(string Owner, string Email, string TicketCode, int MovieID, int Amount, int SeatX, int SeatY, int DateID, int Hall, double TotalPrice, int HallID)
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
                seatXParam.Value = SeatX;
                seatYParam.Value = SeatY;
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

		/// <summary>
		/// Delete a ticket with a ticketcode
		/// </summary>
		/// <param name="Ticketcode">The code of a ticket</param>
        public void DeleteReservationWithTicket(string Ticketcode)
        {
            try
            {
				AdminData AD = new AdminData();
				ShowData SD = new ShowData();

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
					int dateID;
					bool isFound;
					int deletedAmount;
					int foundAmount;
					double TotalPrice;
					int amountofticketscounted;
					int amountoftickets;

					while (true)
					{
						isFound = false;
						deletedAmount = 0;
						foundAmount = 0;
						amountofticketscounted = 0;
						amountoftickets = dataTable.Rows.Count;

						foreach (DataRow row in dataTable.Rows)
						{
							amountofticketscounted += 1;
							TicketCode = row["TicketCode"].ToString();
							TicketID = row["TicketID"].ToString();
							MovieID = row["MovieID"].ToString();
							DateID = row["DateID"].ToString();
							hallID = Convert.ToInt32(row["HallID"]);
							amount = Convert.ToInt32(row["amount"]);
							seatX = Convert.ToInt32(row["seatX"]);
							seatY = Convert.ToInt32(row["seatY"]);
							dateID = Convert.ToInt32(row["DateID"]);
							TotalPrice = Convert.ToDouble(row["TotalPrice"]);
							double pricedelete = -TotalPrice;

							if (TicketCode == Ticketcode)
							{
								// Ticket and contact information overview to check if you want to remove the right ticket.
								Console.Clear();
								SD.Overview(TicketID, MovieID, DateID);
								isFound = true;
								foundAmount += 1;

								Console.WriteLine("\nDo you want to remove this reservation?\n[1] Yes, remove reservation\n[2] No");
								string CancelOrDelete = Console.ReadLine();

								if (CancelOrDelete.Length > 5)
								{
									SD.ClearAndErrorMessage("Your input is too big");
								}
								else if (CancelOrDelete == "1")
								{
									TicketCodeParam.Value = Ticketcode;
									command.Parameters.Add(TicketCodeParam);
									command.Prepare();
									command.ExecuteNonQuery();
									
									DateTime MonthYear = AD.GetDate(dateID);
									Connection.Close();
									var MonthMM = Convert.ToDateTime(MonthYear).ToString("MM");
									int Month = Convert.ToInt32(MonthMM);
									var Yearyyyy = Convert.ToDateTime(MonthYear).ToString("yyyy");
									int Year = Convert.ToInt32(Yearyyyy);

									AD.UpdateRevenueYear(Year, pricedelete);
									AD.UpdateRevenueMonth(Month, Year, pricedelete);

									// This set the seats back to available
									AD.switchAvail((seatX - 1), (seatY - 1), hallID, amount, true);
									deletedAmount += 1;
								}

								else if (CancelOrDelete == "2")
								{
									Console.Clear();
								}
							}
						}

						// check if all tickets were checked
						if (amountoftickets == amountofticketscounted)
						{
							if (isFound)
							{
								if (deletedAmount > 0)
								{
									Console.WriteLine(deletedAmount.ToString() + " reservation(s) out of " + foundAmount.ToString() + " removed. Press enter to continue");
								}
								else
								{
									Console.WriteLine("\nReservation(s) not removed. Press enter to continue");
								}

							}

							else
							{
								Console.Clear();
								Console.WriteLine("\nThere were no results found with the ticketcode: " + Ticketcode + "\nPress enter to go back to the menu");
							}
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

		/// <summary>
		/// delete a ticket with an emailadress
		/// </summary>
		/// <param name="EmailAddress">given email address</param>
		public void DeleteReservationWithEmail(string EmailAddress)
        {
			try
			{
				AdminData AD = new AdminData();
				ShowData SD = new ShowData();
				int seatX = 0;
				int seatY = 0;
				int hallID;
				int amount;

				Connection.Open();

				string stringToDelete = @"DELETE FROM ticket WHERE TicketID = @TicketID";
				string TicketInfo = @"SELECT * FROM ticket";

				MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
				MySqlParameter TicketIDParam = new MySqlParameter("@TicketID", MySqlDbType.String);
				MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);

				using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
				{
					DataTable dataTable = new DataTable();

					dataTable.Load(getTicketInfo);
					string Email;
					string TicketID;
					string MovieID;
					string DateID;
					int dateid;
					bool isFound;
					int deletedAmount;
					int foundAmount;
					double TotalPrice;
					int amountofticketscounted;
					int amountoftickets;

					while (true)
					{
						isFound = false;
						deletedAmount = 0;
						foundAmount = 0;
						amountofticketscounted = 0;
						amountoftickets = dataTable.Rows.Count;

						foreach (DataRow row in dataTable.Rows)
						{
							amountofticketscounted += 1;
							Email = row["Email"].ToString();
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

							if (Email == EmailAddress)
							{
								// Ticket and contact information overview to check if you want to remove the right ticket.
								Console.Clear();
								SD.Overview(TicketID, MovieID, DateID);
								isFound = true;
								foundAmount += 1;

								Console.WriteLine("\nDo you want to remove this reservation?\n[1] Yes, remove reservation\n[2] No");
								string CancelOrDelete = Console.ReadLine();


								if (CancelOrDelete == "1")
								{
									TicketIDParam.Value = TicketID;
									command.Parameters.Add(TicketIDParam);
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
									deletedAmount += 1;
								}

								else if (CancelOrDelete == "2")
								{
									Console.Clear();
								}
							}
						}

						// check if all tickets were checked
						if (amountoftickets == amountofticketscounted)
						{
							if (isFound)
							{
								if (deletedAmount > 0)
								{
									Console.WriteLine(deletedAmount.ToString() + " reservation(s) out of " + foundAmount.ToString() + " removed. Press enter to continue");
								}
								else
								{
									Console.WriteLine("\nReservation(s) not removed. Press enter to continue");
								}

							}

							else
							{
								Console.Clear();
								Console.WriteLine("\nThere were no results found with the emailaddress: " + EmailAddress + "\nPress enter to go back to the menu");
							}
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

		/// <summary>
		/// delete a ticket with an name
		/// </summary>
		/// <param name="full">given full name</param>
		public void DeleteReservationWithName(string FullName)
		{
			try
			{
				AdminData AD = new AdminData();
				ShowData SD = new ShowData();

				int seatX = 0;
				int seatY = 0;
				int hallID;
				int amount;

				Connection.Open();

				string stringToDelete = @"DELETE FROM ticket WHERE TicketID = @TicketID";
				string TicketInfo = @"SELECT * FROM ticket";

				MySqlCommand command = new MySqlCommand(stringToDelete, Connection);
				MySqlParameter TicketIDParam = new MySqlParameter("@TicketID", MySqlDbType.String);
				MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);

				using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
				{
					DataTable dataTable = new DataTable();

					dataTable.Load(getTicketInfo);
					string Owner;
					string TicketID;
					string MovieID;
					string DateID;
					int dateid;
					bool isFound;
					int deletedAmount;
					int foundAmount;
					double TotalPrice;
					int amountofticketscounted;
					int amountoftickets;

					while (true)
					{
						isFound = false;
						deletedAmount = 0;
						foundAmount = 0;
						amountofticketscounted = 0;
						amountoftickets = dataTable.Rows.Count;

						foreach (DataRow row in dataTable.Rows)
						{
							amountofticketscounted += 1;
							Owner = row["Owner"].ToString();
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

							if (Owner == FullName)
							{
								// Ticket and contact information overview to check if you want to remove the right ticket.
								Console.Clear();
								SD.Overview(TicketID, MovieID, DateID);
								isFound = true;
								foundAmount += 1;

								Console.WriteLine("\nDo you want to remove this reservation?\n[1] Yes, remove reservation\n[2] No");
								string CancelOrDelete = Console.ReadLine();

								if (CancelOrDelete == "1")
								{
									TicketIDParam.Value = TicketID;
									command.Parameters.Add(TicketIDParam);
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
									deletedAmount += 1;
								}

								else if (CancelOrDelete == "2")
								{
									Console.Clear();
								}
							}
						}

						// check if all tickets were checked
						if (amountoftickets == amountofticketscounted)
						{
							if (isFound)
							{
								if (deletedAmount > 0)
								{
									Console.WriteLine(deletedAmount.ToString() + " reservation(s) out of "+ foundAmount.ToString() + " removed. Press enter to continue");
								}
								else
                                {
									Console.WriteLine("\nReservation(s) not removed. Press enter to continue");
								}
								
							}
							
							else
							{
								Console.Clear();
								Console.WriteLine("\nThere were no results found with name: " + FullName + "\nPress enter to go back to the menu");
							}
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
		
		/// <summary>
		/// Check how much reservations there are at the moment
		/// </summary>
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

			}
			catch (MySqlException ex)
			{
				throw;
			}
			finally
			{
				Connection.Close();
				Console.WriteLine("Press [enter] to continue.");
				Console.ReadLine();
				Console.Clear();
			}
		}

		/// <summary>
		/// Check if a product exists
		/// </summary>
		/// <param name="ItemID">ID of the product you want to look up</param>
		/// <returns>If the product exists or not</returns>
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