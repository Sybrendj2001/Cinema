using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using CinemaConsole.Pages.Customer;

namespace CinemaConsole.Data.BackEnd
{
    class ShowData : Connecter
    {
        /// <summary>
        /// show all movies from the db
        /// </summary>
         public void ShowMovies()
         {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);

                // creating the strings 
                string movieID;
                string movieName;
                string movieYear;

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        movieID = row["MovieID"].ToString();
                        movieName = row["MovieName"].ToString();
                        movieYear = row["MovieYear"].ToString();
                        Console.WriteLine("[" + movieID + "] " + movieName + " (" + movieYear + ")");
                    }
                    Console.WriteLine("\nEnter the number of the movie for details:");
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
        /// Show the extra movie info with the right ID
        /// </summary>
        /// <param name="movieID">given movie id</param>
        public string ShowMovieByID(string movieID)
        {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie WHERE MovieID = @id";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);
                oCmd.Parameters.AddWithValue("@id", movieID);

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("\nMovie selected: " + row["MovieName"].ToString());
                        Console.WriteLine("Year: " + row["MovieYear"].ToString());
                        Console.WriteLine("Age restriction: " + row["MovieMinimumAge"].ToString() + "+");
                        Console.WriteLine("Actors: " + row["MovieActors"].ToString());
                        Console.WriteLine("Summary: " + row["MovieSummary"].ToString());

                        // show the times with the id of the movie
                        return row["MovieID"].ToString();
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
            return "";
        }
        /// <summary>
        /// show the right times for the right movie
        /// </summary>
        /// <param name="movieID">given movie id</param>
        public void ShowTimesByMovieID(string movieID, string CustomerTimeOption)
        {
            while (true)
            {
                if (CustomerTimeOption == "1")
                {
                    try
                    {
                        Connection.Open();

                        string queryDateTime = @"SELECT * from date WHERE movieID = @movieid";
                        MySqlCommand command = new MySqlCommand(queryDateTime, Connection);
                        command.Parameters.AddWithValue("@movieid", movieID);
                        using (MySqlDataReader getDateTimeInfo = command.ExecuteReader())
                        {
                            int totalRows = getDateTimeInfo.FieldCount;
                            int dateNumber = 0;
                            Console.WriteLine("");

                            while (getDateTimeInfo.Read())
                            {
                                dateNumber += 1;
                                Console.WriteLine("[" + dateNumber + "] " + getDateTimeInfo["DateTime"] + "      Theaterhall " + getDateTimeInfo["Hall"]);
                            }

                            Console.WriteLine("[exit] Back to menu");
                            break;
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
                else if (CustomerTimeOption == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nWould you like to see the dates and times? \n[1] Yes\n[exit] To return to movielist");
                }
            }
        }
    }
}
