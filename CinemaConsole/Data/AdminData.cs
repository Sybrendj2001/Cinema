using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace CinemaConsole.Data
{
    public class AdminData : Connecter
    {
        /// <summary>
        /// Gets the title from a movie
        /// </summary>
        /// <param name="MovieID">ID that needs to be searched</param>
        /// <returns>A Title</returns>
        public string getTitle(int MovieID)
        {
            string title = "";
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT MovieName FROM movie WHERE MovieID = @MovieID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.VarChar);

                MovieIDParam.Value = MovieID;

                command.Parameters.Add(MovieIDParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    title = dataReader.GetString("MovieName");
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
            return title;
        }

        /// <summary>
        /// Searches for the specific hall for a movie without a connection
        /// </summary>
        /// <param name="DateID">ID that hall was originally provided with</param>
        /// <returns>A Hall ID</returns>
        public int GetHallIDConnectless(int DateID)
        {
            int HallID = -1;
            try
            {
                string IntToCheck = @"SELECT HallID FROM hall WHERE DateID = @DateID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter DateIDParam = new MySqlParameter("@DateID", MySqlDbType.VarChar);

                DateIDParam.Value = DateID;

                command.Parameters.Add(DateIDParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    HallID = dataReader.GetInt32("HallID");
                }
                dataReader.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
            return HallID;
        }

        /// <summary>
        /// Searches for the specific hall for a movie with a connection
        /// </summary>
        /// <param name="DateID">ID that Hall was originally provided with</param>
        /// <returns>A Hall ID</returns>
        public int GetHallID(int DateID)
        {
            int HallID = -1;
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT HallID FROM hall WHERE DateID = @DateID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter DateIDParam = new MySqlParameter("@DateID", MySqlDbType.VarChar);

                DateIDParam.Value = DateID;

                command.Parameters.Add(DateIDParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    HallID = dataReader.GetInt32("HallID");
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
            return HallID;
        }

        /// <summary>
        /// Searches for halls with a specific ID
        /// </summary>
        /// <param name="MovieID">Specific ID to search</param>
        /// <returns>Two lists of halls with IDs</returns>
        public Tuple<List<int>, List<int>> GetDateHallIDs(int MovieID)
        {
            List<int> DateIDs = new List<int>();
            List<int> HallIDs = new List<int>();
            try
            {
                Connection.Open();

                string SelectDate = @"SELECT DateID FROM date WHERE MovieID = @MovieID";

                MySqlCommand commandDate = new MySqlCommand(SelectDate, Connection);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.Int32);

                MovieIDParam.Value = MovieID;

                commandDate.Parameters.Add(MovieIDParam);

                MySqlDataReader dataReader = commandDate.ExecuteReader();

                while (dataReader.Read())
                {
                    DateIDs.Add(dataReader.GetInt32("DateID"));
                }
                dataReader.Close();

                for (int i = 0; i < DateIDs.Count; i++)
                {
                    string SelectHall = @"SELECT HallID FROM hall WHERE DateID = @dateID";

                    MySqlCommand commandHall = new MySqlCommand(SelectHall, Connection);
                    MySqlParameter DateIDParam = new MySqlParameter("@dateID", MySqlDbType.Int32);

                    DateIDParam.Value = DateIDs[i];

                    commandHall.Parameters.Add(DateIDParam);

                    MySqlDataReader dataReader2 = commandHall.ExecuteReader();

                    while (dataReader2.Read())
                    {
                        HallIDs.Add(dataReader2.GetInt32("HallID"));
                    }
                    dataReader2.Close();
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
            return Tuple.Create(DateIDs, HallIDs);
        }

        /// <summary>
        /// Gets one specific id where you can provide the date and hall
        /// </summary>
        /// <param name="Date">Date in Year/Month/Day</param>
        /// <param name="Hall">Type of hall</param>
        /// <returns>A ID of the specific date you are searchig for</returns>
        public int GetDateID(DateTime Date, int Hall)
        {
            int DateID = -1;
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT DateID FROM date WHERE Hall = @Hall AND DateTime = @DataTime";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);
                MySqlParameter DateTimeParam = new MySqlParameter("@DataTime", MySqlDbType.DateTime);

                HallParam.Value = Hall;
                DateTimeParam.Value = Date;

                command.Parameters.Add(HallParam);
                command.Parameters.Add(DateTimeParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    DateID = dataReader.GetInt32("DateID");
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
            return DateID;
        }

        /// <summary>
        /// Gets one specific id where you can provide the moviename
        /// </summary>
        /// <param name="MovieName">Name of the movie</param>
        /// <returns>A movie ID</returns>
        public int GetMovieID(string MovieName)
        {
            int movieID = -1;
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT MovieID FROM movie WHERE MovieName = @MovieName";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter MovieNameParam = new MySqlParameter("@MovieName", MySqlDbType.VarChar);

                MovieNameParam.Value = MovieName;

                command.Parameters.Add(MovieNameParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    movieID = dataReader.GetInt32("MovieID");
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
            return movieID;
        }

        /// <summary>
        /// Creates a new date for a movie that is provided
        /// </summary>
        /// <param name="DT">Date you want</param>
        /// <param name="MovieID">The ID of the movie</param>
        /// <param name="Hall">The hall the movie will be displayed in</param>
        public void CreateDateTime(DateTime DT, int MovieID, int Hall)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO date (MovieID, DateTime, Hall) VALUES (@MovieID, @DateTime, @Hall)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.Double);
                MySqlParameter DTParam = new MySqlParameter("@DateTime", MySqlDbType.DateTime);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);

                MovieIDParam.Value = MovieID;
                DTParam.Value = DT;
                HallParam.Value = Hall;

                command.Parameters.Add(MovieIDParam);
                command.Parameters.Add(DTParam);
                command.Parameters.Add(HallParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                
                if (ex.Message.Contains("Duplicate"))
                {
                    Console.WriteLine("You already have a movie on this time in this hall");
                }
                //throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// Create a new hall with all the specifications
        /// </summary>
        /// <param name="SeatAmount">The total amount of seats</param>
        /// <param name="RowLength">The length of rows</param>
        /// <param name="ColLength">The amount of rows there are</param>
        /// <param name="DateID">The date you want to use the hall</param>
        /// <param name="InnerCircle">The price of the inner circle</param>
        /// <param name="MiddleCircle">The price of the middle circle</param>
        /// <param name="OuterCircle">The price of the outer circle</param>
        public void CreateHall(int SeatAmount, int RowLength, int ColLength, int DateID, double InnerCircle, double MiddleCircle, double OuterCircle)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO hall (SeatsAmount, RowLength, ColLength, DateID, InnerCircle, MiddleCircle, OuterCircle) VALUES (@SeatAmount, @RowLength, @ColLength, @DateID, @InnerCircle, @MiddleCircle, @OuterCircle)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter SeatAmountParam = new MySqlParameter("@SeatAmount", MySqlDbType.Int32);
                MySqlParameter RowLengthParam = new MySqlParameter("@RowLength", MySqlDbType.Int32);
                MySqlParameter ColLengthParam = new MySqlParameter("@ColLength", MySqlDbType.Int32);
                MySqlParameter DateIDParam = new MySqlParameter("@DateID", MySqlDbType.Int32);
                MySqlParameter InnerParam = new MySqlParameter("@InnerCircle", MySqlDbType.Double);
                MySqlParameter MiddleParam = new MySqlParameter("@MiddleCircle", MySqlDbType.Double);
                MySqlParameter OuterParam = new MySqlParameter("@OuterCircle", MySqlDbType.Double);

                SeatAmountParam.Value = SeatAmount;
                RowLengthParam.Value = RowLength;
                ColLengthParam.Value = ColLength;
                DateIDParam.Value = DateID;
                InnerParam.Value = InnerCircle;
                MiddleParam.Value = MiddleCircle;
                OuterParam.Value = OuterCircle;

                command.Parameters.Add(SeatAmountParam);
                command.Parameters.Add(RowLengthParam);
                command.Parameters.Add(ColLengthParam);
                command.Parameters.Add(DateIDParam);
                command.Parameters.Add(InnerParam);
                command.Parameters.Add(MiddleParam);
                command.Parameters.Add(OuterParam);

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
        /// Inserts a seat in the database.
        /// </summary>
        /// <param name="price">The price of a seat</param>
        /// <param name="Y">The Position of the seat</param>
        /// <param name="X">The Position of the seat</param>
        /// <param name="HallID">The ID of the hall where the movie will be displayed</param>
        /// <param name="Avail">The availability of the seat</param>
        /// <param name="Name">The name of the customer</param>
        public void CreateSeat(double price, int Y, int X, int HallID, bool Avail, string Name)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO seats (Price, RowSeat, ColumnSeat, HallID, Availability, Name) VALUES (@Price, @RowSeat, @ColumnSeat, @Hall, @Availability, @Name)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter priceParam = new MySqlParameter("@Price", MySqlDbType.Double);
                MySqlParameter RowSeatParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                MySqlParameter colSeatParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);
                MySqlParameter availParam = new MySqlParameter("@Availability", MySqlDbType.Bit);
                MySqlParameter NameParam = new MySqlParameter("@Name", MySqlDbType.VarChar);

                priceParam.Value = price;
                RowSeatParam.Value = Y;
                colSeatParam.Value = X;
                HallParam.Value = HallID;
                NameParam.Value = Name;
                if (Avail)
                {
                    availParam.Value = 1;
                }
                else
                {
                    availParam.Value = 0;
                }

                command.Parameters.Add(priceParam);
                command.Parameters.Add(RowSeatParam);
                command.Parameters.Add(colSeatParam);
                command.Parameters.Add(HallParam);
                command.Parameters.Add(NameParam);
                command.Parameters.Add(availParam);

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
        /// Gets all the times a movie is played
        /// </summary>
        /// <param name="MovieID">Movie you want to chech</param>
        /// <returns>Returns a list with times and halls</returns>
        public Tuple<List<DateTime>, List<int>, List<int>> GetTime(int MovieID)
        {
            List<DateTime> dt = new List<DateTime>();
            List<int> dateID = new List<int>();
            List<int> Hall = new List<int>();
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT DateTime, DateID, Hall FROM date WHERE MovieID = @MovieID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter MovieIDParam = new MySqlParameter("@MovieID", MySqlDbType.Int32);

                MovieIDParam.Value = MovieID;

                command.Parameters.Add(MovieIDParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    dt.Add(dataReader.GetDateTime("DateTime"));
                    dateID.Add(dataReader.GetInt32("DateID"));
                    Hall.Add(dataReader.GetInt32("Hall"));
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
            return Tuple.Create(dt, dateID, Hall);
        }

        /// <summary>
        /// Gets all the seats from 1 specific hall
        /// </summary>
        /// <param name="HallID">The hall you want to search all the seats for</param>
        /// <returns>A list of seats with availability, prices and place</returns>
        public List<Tuple<double, int, int, string, bool>> GetSeat(int HallID)
        {
            List<Tuple<double, int, int, string, bool>> seat = new List<Tuple<double, int, int, string, bool>>();
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT * FROM seats WHERE HallID = @HallID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter HallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                HallIDParam.Value = HallID;

                command.Parameters.Add(HallIDParam);


                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    double price = dataReader.GetDouble("price");
                    int Y = dataReader.GetInt32("RowSeat");
                    int X = dataReader.GetInt32("ColumnSeat");
                    string Name = dataReader.GetString("Name");
                    bool avail = false;
                    if (dataReader.GetInt32("Availability") == 1)
                    {
                        avail = true;
                    }

                    seat.Add(Tuple.Create(price, Y, X, Name, avail));
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
            return seat;
        }

        /// <summary>
        /// Gets all the info about a specific hall
        /// </summary>
        /// <param name="HallID">The hall you want to search all the info about</param>
        /// <returns>A Tuple with all the seats and prices</returns>
        public Tuple<int, int, int, int, double, double, double> GetHallInfo(int HallID)
        {
            int row = 0;
            int col = 0;
            int dateID = 0;
            double inner = 0;
            double middle = 0;
            double outer = 0;

            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT * FROM hall WHERE HallID = @HallID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter HallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                HallIDParam.Value = HallID;

                command.Parameters.Add(HallIDParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    row = dataReader.GetInt32("RowLength");
                    col = dataReader.GetInt32("ColLength");
                    dateID = dataReader.GetInt32("DateID");
                    inner = dataReader.GetDouble("InnerCircle");
                    middle = dataReader.GetDouble("MiddleCircle");
                    outer = dataReader.GetDouble("OuterCircle");
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

            return Tuple.Create(row, col, dateID, HallID, inner, middle, outer);
        }

        /// <summary>
        /// Switch a seat availability
        /// </summary>
        /// <param name="SeatX">Place where seat reservation starts</param>
        /// <param name="SeatY">Place where seat reservation starts</param>
        /// <param name="HallID">The hall you want to search</param>
        /// <param name="Amount">The amount of seats you want to switch</param>
        /// <param name="Avail">The availability you want to change to</param>
        public void switchAvail(int SeatX, int SeatY, int HallID, int Amount, bool Avail)
        {
            try
            {
                int count = 0;
                Connection.Open();
                while (true)
                {
                    string stringToUpdate = @"UPDATE seats SET Availability = @Availability WHERE RowSeat = @seatY AND ColumnSeat = @seatX AND HallID = @HallID";

                    MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);

                    MySqlParameter availparam = new MySqlParameter("@Availability", MySqlDbType.Bit);
                    MySqlParameter Yparam = new MySqlParameter("@seatY", MySqlDbType.Int32);
                    MySqlParameter Xparam = new MySqlParameter("@seatX", MySqlDbType.Int32);
                    MySqlParameter IDparam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                    if (Avail)
                    {
                        availparam.Value = 1;
                    }
                    else
                    {
                        availparam.Value = 0;
                    }

                    Yparam.Value = SeatY;
                    Xparam.Value = (SeatX + count);
                    IDparam.Value = HallID;

                    command.Parameters.Add(availparam);
                    command.Parameters.Add(Yparam);
                    command.Parameters.Add(Xparam);
                    command.Parameters.Add(IDparam);

                    command.Prepare();
                    command.ExecuteNonQuery();

                    count++;
                    if (count >= Amount)
                    {
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
        /// Delete a movie
        /// </summary>
        /// <param name="MovieID">ID of the movie you want to delete</param>
        public void DeleteMovie(int MovieID)
        {
            Tuple<List<int>, List<int>> dateHallIDs = GetDateHallIDs(MovieID);

            try
            {
                Connection.Open();
                string DeleteMovie = @"DELETE FROM movie WHERE MovieID = @ID";

                MySqlParameter movieIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                movieIDParam.Value = MovieID;

                //Delete movie with movieID
                MySqlCommand commandMovie = new MySqlCommand(DeleteMovie, Connection);

                commandMovie.Parameters.Add(movieIDParam);

                commandMovie.Prepare();
                commandMovie.ExecuteNonQuery();

                //Delete dates with movieID
                string DeleteDate = @"DELETE FROM date WHERE MovieID = @ID";
                MySqlCommand commandDate = new MySqlCommand(DeleteDate, Connection);

                commandDate.Parameters.Add(movieIDParam);

                commandDate.Prepare();
                commandDate.ExecuteNonQuery();

                for (int i = 0; i < dateHallIDs.Item1.Count; i++)
                {


                    MySqlParameter dateIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                    dateIDParam.Value = dateHallIDs.Item1[i];

                    //Delete halls with dateIDs
                    string DeleteHall = @"DELETE FROM hall WHERE DateID = @ID";
                    MySqlCommand commandhall = new MySqlCommand(DeleteHall, Connection);

                    commandhall.Parameters.Add(dateIDParam);

                    commandhall.Prepare();
                    commandhall.ExecuteNonQuery();

                    //Delete tickets with dateIDs
                    string DeleteTicket = @"DELETE FROM ticket WHERE DateID = @ID";
                    MySqlCommand commandTicket = new MySqlCommand(DeleteTicket, Connection);

                    commandTicket.Parameters.Add(dateIDParam);

                    commandTicket.Prepare();
                    commandTicket.ExecuteNonQuery();
                }

                for (int i = 0; i < dateHallIDs.Item2.Count; i++)
                {
                    MySqlParameter hallIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                    hallIDParam.Value = dateHallIDs.Item2[i];

                    string DeleteSeat = @"DELETE FROM seats WHERE HallID = @ID";
                    MySqlCommand commandSeat = new MySqlCommand(DeleteSeat, Connection);

                    commandSeat.Parameters.Add(hallIDParam);

                    commandSeat.Prepare();
                    commandSeat.ExecuteNonQuery();
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
        /// Delete 1 specific time
        /// </summary>
        /// <param name="DateID">ID of time you want to delete</param>
        public void DeleteTime(int DateID)
        {
            int hallID = GetHallID(DateID);

            try
            {
                Connection.Open();

                MySqlParameter dateIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                dateIDParam.Value = DateID;

                //Delete date with DateID
                string DeleteDate = @"DELETE FROM date WHERE DateID = @ID";
                MySqlCommand commandDate = new MySqlCommand(DeleteDate, Connection);

                commandDate.Parameters.Add(dateIDParam);

                commandDate.Prepare();
                commandDate.ExecuteNonQuery();

                //Delete hall with DateID
                string DeleteHall = @"DELETE FROM hall WHERE DateID = @ID";
                MySqlCommand commandhall = new MySqlCommand(DeleteHall, Connection);

                commandhall.Parameters.Add(dateIDParam);

                commandhall.Prepare();
                commandhall.ExecuteNonQuery();

                //Delete tickets with dateID
                string DeleteTicket = @"DELETE FROM ticket WHERE DateID = @ID";
                MySqlCommand commandTicket = new MySqlCommand(DeleteTicket, Connection);

                commandTicket.Parameters.Add(dateIDParam);

                commandTicket.Prepare();
                commandTicket.ExecuteNonQuery();

                //Delete seats met hallID
                string DeleteSeat = @"DELETE FROM seats WHERE HallID = @ID";

                MySqlCommand commandSeat = new MySqlCommand(DeleteSeat, Connection);
                MySqlParameter hallIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);

                hallIDParam.Value = hallID;

                commandSeat.Parameters.Add(hallIDParam);

                commandSeat.Prepare();
                commandSeat.ExecuteNonQuery();
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
        /// Delete all the times that have past
        /// </summary>
        public void DeletepastTimes()
        {
            try
            {
                List<int> DateIDs = new List<int>();
                Connection.Open();

                string StringtoRead = @"SELECT DateID, DateTime FROM date";

                MySqlCommand commandRead = new MySqlCommand(StringtoRead, Connection);

                MySqlDataReader dataReader = commandRead.ExecuteReader();

                while (dataReader.Read())
                {
                    if (dataReader.GetDateTime("DateTime") < DateTime.Now)
                    {
                        DateIDs.Add(dataReader.GetInt32("DateID"));
                    }
                }
                dataReader.Close();

                int hallID;

                for (int i = 0; i < DateIDs.Count; i++)
                {
                    hallID = GetHallIDConnectless(DateIDs[i]);

                    MySqlParameter dateIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                    dateIDParam.Value = DateIDs[i];

                    //Delete date with DateID
                    string DeleteDate = @"DELETE FROM date WHERE DateID = @ID";
                    MySqlCommand commandDate = new MySqlCommand(DeleteDate, Connection);

                    commandDate.Parameters.Add(dateIDParam);

                    commandDate.Prepare();
                    commandDate.ExecuteNonQuery();

                    //Delete hall with DateID
                    string DeleteHall = @"DELETE FROM hall WHERE DateID = @ID";
                    MySqlCommand commandhall = new MySqlCommand(DeleteHall, Connection);

                    commandhall.Parameters.Add(dateIDParam);

                    commandhall.Prepare();
                    commandhall.ExecuteNonQuery();

                    //Delete tickets with dateID
                    string DeleteTicket = @"DELETE FROM ticket WHERE DateID = @ID";
                    MySqlCommand commandTicket = new MySqlCommand(DeleteTicket, Connection);

                    commandTicket.Parameters.Add(dateIDParam);

                    commandTicket.Prepare();
                    commandTicket.ExecuteNonQuery();

                    //Delete seats met hallID
                    string DeleteSeat = @"DELETE FROM seats WHERE HallID = @ID";

                    MySqlCommand commandSeat = new MySqlCommand(DeleteSeat, Connection);
                    MySqlParameter hallIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);

                    hallIDParam.Value = hallID;

                    commandSeat.Parameters.Add(hallIDParam);

                    commandSeat.Prepare();
                    commandSeat.ExecuteNonQuery();
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
        /// Checks for the duration of a movie
        /// </summary>
        /// <param name="MovieID">ID of the movie you want to check</param>
        /// <returns>Returns the duration of the of a </returns>
        public int getDuration(int MovieID)
        {
            int duration = 0;
            try
            {
                Connection.Open();
                string query = @"SELECT MovieDuration FROM movie WHERE MovieID = @ID";

                MySqlCommand command = new MySqlCommand(query, Connection);

                MySqlParameter ParamID = new MySqlParameter("@ID", MySqlDbType.Int32);

                ParamID.Value = MovieID;

                command.Parameters.Add(ParamID);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    duration = reader.GetInt32("MovieDuration");
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close();
            }
            return duration;
        }

        /// <summary>
        /// Gets all the dates that exist
        /// </summary>
        /// <returns>3 lists with all the dates that exist</returns>
        public Tuple<List<DateTime>, List<int>, List<DateTime>> GetAllDates()
        {
            List<DateTime> StartTime = new List<DateTime>();
            List<int> Hall = new List<int>();
            List<DateTime> Endtime = new List<DateTime>();
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT date.DateTime, date.hall, movie.MovieDuration FROM Cinema.date LEFT JOIN Cinema.movie ON date.MovieID = movie.MovieID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    StartTime.Add(dataReader.GetDateTime("DateTime"));
                    Hall.Add(dataReader.GetInt32("Hall"));
                    Endtime.Add(dataReader.GetDateTime("DateTime").AddMinutes(dataReader.GetInt32("MovieDuration")));
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
            return Tuple.Create(StartTime, Hall, Endtime);
        }

        /// <summary>
        /// Returns all the different prices of seats
        /// </summary>
        /// <param name="HallID">Hall you want to search prices for</param>
        /// <returns>All the three different prices</returns>
        public Tuple<double, double, double> getPrices(int HallID)
        {
            double InnerCircle = 0.0;
            double MiddleCircle = 0.0;
            double OuterCircle = 0.0;

            try
            {
                Connection.Open();

                string StringtoRead = @"SELECT * FROM prices WHERE Hall = @hall";

                MySqlCommand command = new MySqlCommand(StringtoRead, Connection);
                MySqlParameter hallParam = new MySqlParameter("@hall", MySqlDbType.Int32);

                hallParam.Value = HallID;

                command.Parameters.Add(hallParam);

                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    InnerCircle = dataReader.GetDouble("InnerCircle");
                    MiddleCircle = dataReader.GetDouble("MiddleCircle");
                    OuterCircle = dataReader.GetDouble("OuterCircle");
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

            return Tuple.Create(InnerCircle, MiddleCircle, OuterCircle);
        }

        /// <summary>
        /// Update a price in a specific hall
        /// </summary>
        /// <param name="Hall">Hall you want to update</param>
        /// <param name="Circle">Circle you want to change</param>
        public void UpdatePrice(int Hall, int Circle)
        {
            ShowData SD = new ShowData();

            try
            {
                Connection.Open();
                double example = 10.50;
                double price = 0.0;
                Console.WriteLine("\nPlease give the price you want. And write it down like in the example (e.g. " + example.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")");
                while (true)
                {
                    try
                    {
                        string priceString = Console.ReadLine();
                        if (priceString.Length > 10)
                        {
                            price = Convert.ToDouble(priceString);
                        }
                        else
                        {
                            Console.WriteLine("You are writting a price that is too large");
                        }
                        if (price > 0.0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease enter a price above 0.00 (e.g. " + example.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")");
                        }
                    }
                    catch (FormatException)
                    {
                        SD.ErrorMessage("\nThe price was not put in correctly.");
                        Console.WriteLine("Please write it down like in the example(e.g. " + example.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ")");
                    }
                }

                MySqlParameter priceParam = new MySqlParameter("@price", MySqlDbType.Double);
                MySqlParameter hallParam = new MySqlParameter("@hall", MySqlDbType.Int32);

                priceParam.Value = price;
                hallParam.Value = Hall;

                if (Circle == 1)
                {
                    string StringtoUpdate = @"UPDATE prices SET InnerCircle = @price WHERE Hall = @hall";

                    MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                    command.Parameters.Add(priceParam);
                    command.Parameters.Add(hallParam);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
                else if (Circle == 2)
                {
                    string StringtoUpdate = @"UPDATE prices SET MiddleCircle = @price WHERE Hall = @hall";

                    MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                    command.Parameters.Add(priceParam);
                    command.Parameters.Add(hallParam);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
                else if (Circle == 3)
                {
                    string StringtoUpdate = @"UPDATE prices SET OuterCircle = @price WHERE Hall = @hall";

                    MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                    command.Parameters.Add(priceParam);
                    command.Parameters.Add(hallParam);

                    command.Prepare();
                    command.ExecuteNonQuery();
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
        /// Update one specific halls price
        /// </summary>
        /// <param name="HallID">Hall you want to update</param>
        /// <param name="Price">New price</param>
        /// <param name="Circle">Circle you want to update</param>
        /// <param name="Hall">Hall you want to update</param>
        public void UpdatePriceSeatHall(int HallID, double Price, int Circle, int Hall)
        {
            Connection.Open();
            try
            {
                UpdatePriceSeat(HallID, Price, Circle, Hall);

                string StringtoUpdate = "";

                if (Circle == 1)
                {
                    StringtoUpdate = @"UPDATE hall SET InnerCircle = @price WHERE HallID = @HallID";
                }
                else if (Circle == 2)
                {
                    StringtoUpdate = @"UPDATE hall SET MiddleCircle = @price WHERE HallID = @HallID";
                }
                else
                {
                    StringtoUpdate = @"UPDATE hall SET OuterCircle = @price WHERE HallID = @HallID";
                }

                MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                MySqlParameter priceParam = new MySqlParameter("@price", MySqlDbType.Double);
                MySqlParameter hallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                priceParam.Value = Price;
                hallIDParam.Value = HallID;

                command.Parameters.Add(priceParam);
                command.Parameters.Add(hallIDParam);

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
        /// Update all prices for one hall
        /// </summary>
        /// <param name="HallID">Hall you want to update</param>
        /// <param name="Price">New price</param>
        /// <param name="Circle">Which circle you want</param>
        /// <param name="Hall">specific</param>
        public void UpdatePriceSeat(int HallID, double Price, int Circle, int Hall)
        {
            try
            {
                string StringtoUpdate = @"UPDATE seats SET Price = @price WHERE RowSeat = @RowSeat AND ColumnSeat = @ColumnSeat AND HallID = @HallID";

                MySqlParameter priceParam = new MySqlParameter("@price", MySqlDbType.Double);
                MySqlParameter hallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                priceParam.Value = Price;
                hallIDParam.Value = HallID;

                if (Hall == 1)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {

                            MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                            MySqlParameter rowParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                            MySqlParameter colParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);

                            rowParam.Value = i;
                            colParam.Value = j;

                            command.Parameters.Add(priceParam);
                            command.Parameters.Add(hallIDParam);
                            command.Parameters.Add(rowParam);
                            command.Parameters.Add(colParam);

                            if ((j == 5 || j == 6) && (i > 4 && i < 9))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 5 || j == 6) && (i > 2 && i < 11))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 4 || j == 7) && (i > 3 && i < 10))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 3 || j == 8) && (i > 4 && i < 9))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (Circle == 3)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                else if (Hall == 2)
                {
                    for (int i = 0; i < 19; i++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                            MySqlParameter rowParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                            MySqlParameter colParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);

                            rowParam.Value = i;
                            colParam.Value = j;

                            command.Parameters.Add(priceParam);
                            command.Parameters.Add(hallIDParam);
                            command.Parameters.Add(rowParam);
                            command.Parameters.Add(colParam);

                            if ((j == 8 || j == 9) && (i > 4 && i < 13))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 7 || j == 10) && (i > 5 && i < 12))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 6 || j == 11) && (i > 6 && i < 11))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j > 5 && j < 12) && (i > 0 && i < 16))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 5 || j == 12) && (i > 1 && i < 14))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 4 || j == 13) && (i > 3 && i < 13))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 3 || j == 14) && (i > 5 && i < 12))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 2 || j == 15) && (i > 7 && i < 11))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (Circle == 3)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                else if (Hall == 3)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                            MySqlParameter rowParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                            MySqlParameter colParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);

                            rowParam.Value = i;
                            colParam.Value = j;

                            command.Parameters.Add(priceParam);
                            command.Parameters.Add(hallIDParam);
                            command.Parameters.Add(rowParam);
                            command.Parameters.Add(colParam);

                            if ((j > 12 && j < 17) && (i > 3 && i < 13))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 12 || j == 17) && (i > 4 && i < 12))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 11 || j == 18) && (i > 5 && i < 12))
                            {
                                if (Circle == 1)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j > 11 && j < 18) && (i > 0 && i < 17))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 10 || j == 11 || j == 18 || j == 19) && (i > 0 && i < 16))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 9 || j == 20) && (i > 0 && i < 15))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 8 || j == 21) && (i > 1 && i < 14))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 7 || j == 22) && (i > 3 && i < 12))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 6 || j == 23) && (i > 5 && i < 11))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else if ((j == 5 || j == 24) && (i > 7 && i < 10))
                            {
                                if (Circle == 2)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (Circle == 3)
                                {
                                    command.Prepare();
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Get a date and price from a ticket
        /// </summary>
        /// <param name="TicketCode">Ticket you want to search for</param>
        /// <returns>Date and price of the ticket</returns>
        public Tuple<double, DateTime> GetDatePrice(string TicketCode)
        {
            double totalPrice = 0.00;
            DateTime date = new DateTime();
            
            try
            {
                Connection.Open();

                string TicketInfo = @"SELECT * FROM ticket";

                MySqlCommand command = new MySqlCommand(TicketInfo, Connection);
                MySqlDataReader getTicketInfo = command.ExecuteReader();
                string ticketcode2 = TicketCode.ToString();

                DataTable dataTable = new DataTable();

                dataTable.Load(getTicketInfo);
                string ticketCode;
                bool isFound = true;

                while (isFound)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ticketCode = row["TicketCode"].ToString();

                        if (ticketcode2 == ticketCode)
                        {
                            totalPrice = Convert.ToDouble(row["TotalPrice"]);
                            int DateID = Convert.ToInt32(row["DateID"]);
                            Connection.Close();
                            date = GetDate(DateID);
                            isFound = false;
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
                //Connection.Close();
            }

            return Tuple.Create(totalPrice, date);
        }

        /// <summary>
        /// Get all the dates
        /// </summary>
        /// <param name="DateID">Date you want to search</param>
        /// <returns>Last date there is available</returns>
        public DateTime GetDate(int DateID)
        {
            try
            {
                Connection.Open();
                string DateInfo = @"SELECT * FROM date";

                MySqlCommand oCmd = new MySqlCommand(DateInfo, Connection);

                MySqlDataReader getDateInfo = oCmd.ExecuteReader();
                DataTable dataTable = new DataTable();

                dataTable.Load(getDateInfo);
                DateTime date = new DateTime();
                while (true)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        int dateindb = Convert.ToInt32(row["DateID"]);

                        string datetime = Convert.ToDateTime(row["DateTime"]).ToString("dd/MM/yyyy HH:mm");

                        if (DateID == dateindb)
                        {
                            date = Convert.ToDateTime(row["DateTime"]);
                            break;
                        }
                    }
                    return date;
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
        /// checks if revenue stream is available
        /// </summary>
        /// <param name="Month">Which month you are searching for</param>
        /// <param name="Year">Which year you are searching for</param>
        /// <returns>two bools if they are</returns>
        public Tuple<bool, bool> EditCreateRev(int Month, int Year)
        {
            bool yearYN = false;
            bool monthYN = false;

            try
            {
                Connection.Open();

                string YearInfo = @"SELECT * FROM revenueyear";
                string MonthInfo = @"SELECT * FROM revenuemonth";

                MySqlCommand command = new MySqlCommand(YearInfo, Connection);
                MySqlDataReader getYeartInfo = command.ExecuteReader();

                DataTable dataTable = new DataTable();

                dataTable.Load(getYeartInfo);
                
                int year;
               
                foreach (DataRow row in dataTable.Rows)
                {
                    year = Convert.ToInt32(row["year"]);

                    if (Year == year)
                    {
                        yearYN = true;
                        break;
                    }
                }
                
                MySqlCommand command2 = new MySqlCommand(MonthInfo, Connection);
                MySqlDataReader getMonthInfo = command2.ExecuteReader();
                DataTable dataTable2 = new DataTable();

                dataTable2.Load(getMonthInfo);
                
                int month;           

                foreach (DataRow row in dataTable2.Rows)
                {
                    month = Convert.ToInt32(row["month"]);
                    year = Convert.ToInt32(row["year"]);

                    if (Month == month && Year == year)
                    {
                        monthYN = true;
                        break;
                    }
                }
            }
            catch(MySqlException)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return Tuple.Create(monthYN, yearYN);
        }

        /// <summary>
        /// Update the revenue for a specific month
        /// </summary>
        /// <param name="Month">Month you want to update</param>
        /// <param name="Year">Year you want to update</param>
        /// <param name="Price">New price</param>
        public void UpdateRevenueMonth(int Month, int Year, double Price)
        {
            try
            {
                Connection.Open();

                string MonthInfo = @"SELECT * FROM revenuemonth";

                MySqlCommand command = new MySqlCommand(MonthInfo, Connection);
                MySqlDataReader getMonthInfo = command.ExecuteReader();
                DataTable dataTable = new DataTable();

                dataTable.Load(getMonthInfo);

                int year;
                int month;
                double OldTotalRevenue = 0.00;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    month = Convert.ToInt32(row["month"]);
                    year = Convert.ToInt32(row["year"]);


                    if (Month == month && Year == year)
                    {
                        OldTotalRevenue = Convert.ToDouble(row["revenue"]);
                        break;
                    }
                }
                
                double NewTotalRevenue = OldTotalRevenue + Price;

                MySqlParameter ParamMonth = new MySqlParameter("@month", MySqlDbType.Int32);
                ParamMonth.Value = Month;

                string stringToUpdate = @"UPDATE revenuemonth SET revenue = @Newrevenue WHERE month = @month";

                MySqlCommand command2 = new MySqlCommand(stringToUpdate, Connection);
                MySqlParameter ParamNewrevenue = new MySqlParameter("@Newrevenue", MySqlDbType.Double);

                ParamNewrevenue.Value = NewTotalRevenue;

                command2.Parameters.Add(ParamMonth);

                command2.Parameters.Add(ParamNewrevenue);

                command2.Prepare();
                command2.ExecuteNonQuery();

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
        /// Update the revenue for specific year
        /// </summary>
        /// <param name="Year">Year you want to update</param>
        /// <param name="Price">Price you want to set it</param>
        public void UpdateRevenueYear(int Year, double Price)
        {
            try
            {
                Connection.Open();

                string YearInfo = @"SELECT * FROM revenueyear";

                MySqlCommand command = new MySqlCommand(YearInfo, Connection);
                MySqlDataReader getYearInfo = command.ExecuteReader();

                DataTable dataTable = new DataTable();

                dataTable.Load(getYearInfo);

                int year;
                bool isFound = true;
                double OldTotalRevenue = 0.00;

                while (isFound)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        year = Convert.ToInt32(row["year"]);


                        if (Year == year)
                        {
                            OldTotalRevenue = Convert.ToDouble(row["revenue"]);
                            isFound = false;
                            break;
                        }
                    }
                }

                double NewTotalRevenue = OldTotalRevenue + Price;

                MySqlParameter ParamYear = new MySqlParameter("@year", MySqlDbType.Int32);
                ParamYear.Value = Year;

                string stringToUpdate = @"UPDATE revenueyear SET revenue = @Newrevenue WHERE year = @year";

                MySqlCommand command2 = new MySqlCommand(stringToUpdate, Connection);
                MySqlParameter ParamNewrevenue = new MySqlParameter("@Newrevenue", MySqlDbType.Double);

                ParamNewrevenue.Value = NewTotalRevenue;

                command2.Parameters.Add(ParamYear);

                command2.Parameters.Add(ParamNewrevenue);

                command2.Prepare();
                command2.ExecuteNonQuery();

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
        /// Get the revenue from one year
        /// </summary>
        /// <param name="Year">Year you want to check</param>
        /// <returns>the revenue that is made that year</returns>
        public Tuple<bool, double> GetYearRevenue(int Year)
        {
            double TotalRevenue = 0.00;
            bool Found = false;
            
            try
            {
                Connection.Open();

                string YearInfo = @"SELECT * FROM revenueyear";

                MySqlCommand command = new MySqlCommand(YearInfo, Connection);
                MySqlDataReader getYearInfo = command.ExecuteReader();

                DataTable dataTable = new DataTable();

                dataTable.Load(getYearInfo);

                int year;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    year = Convert.ToInt32(row["year"]);

                    if (year == Year)
                    {
                        TotalRevenue = Convert.ToDouble(row["revenue"]);
                        Found = true;
                        break;
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
            return Tuple.Create(Found, TotalRevenue);
        }

        /// <summary>
        /// Get the revenue from one month
        /// </summary>
        /// <param name="Month">Month you want to check</param>
        /// <param name="Year">Year you want to check</param>
        /// <returns>The revenue for that is made for that month</returns>
        public Tuple<bool, double> GetMonthRevenue(int Month, int Year)
        {
            double TotalRevenue = 0.00;
            bool Found = false;

            try
            {
                Connection.Open();

                string YearInfo = @"SELECT * FROM revenuemonth";

                MySqlCommand command = new MySqlCommand(YearInfo, Connection);
                MySqlDataReader getYearInfo = command.ExecuteReader();

                DataTable dataTable = new DataTable();

                dataTable.Load(getYearInfo);

                int year;
                int month;

                foreach (DataRow row in dataTable.Rows)
                {
                    year = Convert.ToInt32(row["year"]);
                    month = Convert.ToInt32(row["month"]);

                    if (year == Year && month == Month)
                    {
                        TotalRevenue = Convert.ToDouble(row["revenue"]);
                        Found = true;
                        break;
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
            return Tuple.Create(Found, TotalRevenue);
        }

        /// <summary>
        /// Set the revenue for a month
        /// </summary>
        /// <param name="Month">The month you want to set that revenue</param>
        /// <param name="Year">The year you want to set that revenue</param>
        /// <param name="Revenue">The revenue you want to set it to</param>
        public void RevenueMonth(int Month, int Year, double Revenue)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO revenuemonth (month, year, revenue)VALUES (@month, @year, @revenue)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter MonthParam = new MySqlParameter("@month", MySqlDbType.Int32);
                MySqlParameter YearParam = new MySqlParameter("@year", MySqlDbType.Int32);
                MySqlParameter RevenueParam = new MySqlParameter("@revenue", MySqlDbType.Double);

                MonthParam.Value = Month;
                YearParam.Value = Year;
                RevenueParam.Value = Revenue;

                command.Parameters.Add(MonthParam);
                command.Parameters.Add(YearParam);
                command.Parameters.Add(RevenueParam);

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
        /// Set the revenue for a year
        /// </summary>
        /// <param name="Year">The year you want to sest that revenue</param>
        /// <param name="Revenue">The revenue you want to set it too</param>
        public void RevenueYear(int Year, double Revenue)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO revenueyear (year, revenue)VALUES (@year, @revenue)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter YearParam = new MySqlParameter("@year", MySqlDbType.Int32);
                MySqlParameter RevenueParam = new MySqlParameter("@revenue", MySqlDbType.Double);

                YearParam.Value = Year;
                RevenueParam.Value = Revenue;

                command.Parameters.Add(YearParam);
                command.Parameters.Add(RevenueParam);

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