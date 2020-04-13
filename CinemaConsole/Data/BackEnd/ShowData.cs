using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;

namespace CinemaConsole.Data.BackEnd
{
    class ShowData : Connecter
    {
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

        public void ShowMovieByID(string movieID)
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
    }
}
