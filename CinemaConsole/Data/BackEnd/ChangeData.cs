﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;

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
            try
            {
                Connection.Open();
                string stringToDisplay = @"SELECT * FROM restaurantitems";

                MySqlCommand command = new MySqlCommand(stringToDisplay, Connection);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine("[" + dataReader["ItemID"] + "] " + dataReader["ItemName"] + "    €" + dataReader["Price"]);
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

        public int TotalProducts()
        {
            int productsamount = 0;
            try
            {
                Connection.Open();
                Connection.Open();
                string stringToRead = @"SELECT ItemID FROM restaurantitems";

                MySqlCommand command = new MySqlCommand(stringToRead, Connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    productsamount += 1;
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
            return productsamount;
        }


        public void CreateProduct(int totalproducts, string itemname, double price)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO restaurantitems (ItemID, ItemName, Price) VALUES (@ItemID, @ItemName, @Price)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter ItemIDParam = new MySqlParameter("@ItemID", MySqlDbType.Int32);
                MySqlParameter ItemNameParam = new MySqlParameter("@ItemName", MySqlDbType.VarChar);
                MySqlParameter PriceParam = new MySqlParameter("@Price", MySqlDbType.VarChar);

                ItemIDParam.Value = totalproducts;
                ItemNameParam.Value = itemname;
                PriceParam.Value = price;

                command.Parameters.Add(ItemIDParam);
                command.Parameters.Add(ItemNameParam);
                command.Parameters.Add(PriceParam);

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
       
        public void UpdateProduct(int id = -1, string name = "", double price = -1)
        {
            try
            {
                Connection.Open();
                bool updating = true;
                while (updating)
                {
                    string stringToUpdate = @"UPDATE restaurantitems SET @PlaceType = @NewType WHERE ItemID = @ItemID";

                    MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
                    MySqlParameter ParamID = new MySqlParameter("@ItemID", MySqlDbType.Int32);

                    ParamID.Value = id;
                    command.Parameters.Add(ParamID);

                    MySqlParameter ParamPlaceType = new MySqlParameter("@PlaceType", MySqlDbType.Text);
                    MySqlParameter ParamNewType = new MySqlParameter("@NewType", MySqlDbType.VarChar);

                    if (name != "")
                    {
                        ParamPlaceType.Value = "ItemName";
                        ParamNewType.Value = name;
                        name = "";
                    }
                    else if (price != -1)
                    {
                        ParamPlaceType.Value = "Price";
                        ParamNewType.Value = price;
                        price = -1;
                    }
                    else
                    {
                        updating = false;
                        break;
                    }

                    command.Parameters.Add(ParamPlaceType);
                    command.Parameters.Add(ParamNewType);

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
    }

}
