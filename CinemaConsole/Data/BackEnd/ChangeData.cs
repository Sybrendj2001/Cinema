﻿using System;
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

		public void UpdateMovie(int id = -1, string name = "", int year = -1, int minimumage = -1, string summary = "")
		{
			try
			{
				Connection.Open();
				bool updating = true;
				while (updating)
				{
					string stringToUpdate = @"UPDATE movie SET @PlaceType = @NewType WHERE MovieID = @MovieID";

					MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
					//Define Default Parameter
					MySqlParameter ParamID = new MySqlParameter("@MovieID", MySqlDbType.Int32);
					//Valuate Default Parameter
					ParamID.Value = id;
					//Add Default Parameter to Query
					command.Parameters.Add(ParamID);

					//Create Variable Parameter
					MySqlParameter ParamPlaceType = new MySqlParameter("@PlaceType", MySqlDbType.Text);
					MySqlParameter ParamNewType = new MySqlParameter("@NewType", MySqlDbType.VarChar);

					//Check which variables you need to get
					if (name != "")
					{
						ParamPlaceType.Value = "MovieName";
						ParamNewType.Value = name;
						name = "";
					}
					else if (year != -1)
					{
						ParamPlaceType.Value = "MovieYear";
						ParamNewType.Value = year;
						year = -1;
					}
					else if (minimumage != -1)
					{
						ParamPlaceType.Value = "MovieMinimumAge";
						ParamNewType.Value = minimumage;
						minimumage = -1;
					}
					else if (summary != "")
					{
						ParamPlaceType.Value = "MovieSummary";
						ParamNewType.Value = summary;
						summary = "";
					}
					else
					{
						updating = false;
						break;
					}

					//Add Variable Parameters to Query
					command.Parameters.Add(ParamPlaceType);
					command.Parameters.Add(ParamNewType);

					//Execute Query
					command.Prepare();
					command.ExecuteNonQuery();
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

		public void DisplayProducts()
		{
			Console.OutputEncoding = Encoding.UTF8;

			Console.WriteLine("\nMenu:");
			try
			{
				Connection.Open();
				string stringToDisplay = @"SELECT * FROM restaurantitems";

				MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);

				MySqlDataReader dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					double test = dataReader.GetDouble("Price");
					Console.WriteLine("[" + dataReader["ItemID"] + "] " + dataReader["ItemName"] + "    €" + test.ToString("0.00"));
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

				DisplayProducts();
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

					DisplayProducts();
				}

				else if(name != "" && price == -1)
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

					DisplayProducts();
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

					DisplayProducts();
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

				DisplayProducts();
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

		public void InsertMovie(string name, int year, int mage, int msummary)
		{
			try
			{
				Connection.Open();
				string stringToInsert = "INSERT INTO Movie (MovieName, MovieYear, MovieMinimumAge, MovieSummary) VALUES (@Name, @Year, @MAge, @MSummary)";

				MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
				MySqlParameter NameParam = new MySqlParameter("@Name", MySqlDbType.VarChar);
				MySqlParameter YearParam = new MySqlParameter("@Year", MySqlDbType.Int32);
				MySqlParameter MAgeParam = new MySqlParameter("@MAge", MySqlDbType.Int32);
				MySqlParameter MSummaryParam = new MySqlParameter("@MSummary", MySqlDbType.LongText);

				NameParam.Value = name;
				YearParam.Value = year;
				MAgeParam.Value = mage;
				MSummaryParam.Value = msummary;

				command.Parameters.Add(NameParam);
				command.Parameters.Add(YearParam);
				command.Parameters.Add(MAgeParam);
				command.Parameters.Add(MSummaryParam);

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

        public void ReserveTicket(string Owner, string Email, string TicketCode, int MovieID, int Amount, int seatX, int seatY, int DateID, int Hall, double TotalPrice)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO ticket (Owner, Email, TicketCode, MovieID, Amount, seatX, seatY, DateID, HallID, TotalPrice) VALUES (@Owner, @Email, @TicketCode, @MovieID, @Amount, @seatX, @seatY, @DateID, @HallID, @TotalPrice)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                //MySqlParameter TicketIDParam = new MySqlParameter("@TicketID", MySqlDbType.Int32);
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

                //TicketIDParam.Value = TicketID;
                OwnerParam.Value = Owner;
                EmailParam.Value = Email;
                TicketCodeParam.Value = TicketCode;
                MovieIDParam.Value = MovieID;
                AmountParam.Value = Amount;
                seatXParam.Value = seatX;
                seatYParam.Value = seatY;
                DateIDParam.Value = DateID;
                HallIDParam.Value = Hall;
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
    }
}