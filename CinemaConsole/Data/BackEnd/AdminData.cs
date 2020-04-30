using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using MySqlX.XDevAPI.Relational;

namespace CinemaConsole.Data.BackEnd
{
    public class AdminData : Connecter
    {

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

        public int GetDateID(DateTime date, int hall)
        {
            int DateID = -1;
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT DateID FROM date WHERE Hall = @Hall AND DateTime = @DataTime";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);
                MySqlParameter DateTimeParam = new MySqlParameter("@DataTime", MySqlDbType.DateTime);

                HallParam.Value = hall;
                DateTimeParam.Value = date;

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

        public int GetMovieID(string movieName)
        {
            int movieID = -1;
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT MovieID FROM movie WHERE MovieName = @MovieName";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter MovieNameParam = new MySqlParameter("@MovieName", MySqlDbType.VarChar);

                MovieNameParam.Value = movieName;

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
        /// <param name="price"></param>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        /// <param name="hall"></param>
        /// <param name="avail"></param>
        /// <param name="name"></param>
        public void CreateSeat(double price, int Y, int X, int HallID, bool avail, string name)
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
                NameParam.Value = name;
                if (avail)
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

        public List<Tuple<double, int, int, string, bool>> GetSeat(int hallID)
        {
            List<Tuple<double, int, int, string, bool>> seat = new List<Tuple<double, int, int, string, bool>>();
            try
            {
                Connection.Open();
                string IntToCheck = @"SELECT * FROM seats WHERE HallID = @HallID";

                MySqlCommand command = new MySqlCommand(IntToCheck, Connection);
                MySqlParameter HallIDParam = new MySqlParameter("@HallID", MySqlDbType.Int32);

                HallIDParam.Value = hallID;

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

        public void switchAvail(int seatX, int seatY, int hallID, int amount, bool avail)
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

                    if (avail)
                    {
                        availparam.Value = 1;
                    }
                    else
                    {
                        availparam.Value = 0;
                    }

                    Yparam.Value = seatY;
                    Xparam.Value = (seatX + count);
                    IDparam.Value = hallID;

                    command.Parameters.Add(availparam);
                    command.Parameters.Add(Yparam);
                    command.Parameters.Add(Xparam);
                    command.Parameters.Add(IDparam);

                    command.Prepare();
                    command.ExecuteNonQuery();

                    count++;
                    if (count >= amount)
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

        public void DeleteMovie(int movieID)
        {
            Tuple<List<int>, List<int>> dateHallIDs = GetDateHallIDs(movieID);

            try
            {
                Connection.Open();
                string DeleteMovie = @"DELETE FROM movie WHERE MovieID = @ID";

                MySqlParameter movieIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                movieIDParam.Value = movieID;

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

        public void DeleteTime(int dateID)
        {
            int hallID = GetHallID(dateID);

            try
            {
                Connection.Open();

                MySqlParameter dateIDParam = new MySqlParameter("@ID", MySqlDbType.Int32);
                dateIDParam.Value = dateID;

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

        public Tuple<double, double, double> getPrices(int hall)
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

                hallParam.Value = hall;

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

        public void UpdatePrice(int hall, int circle)
        {
            try
            {
                Connection.Open();

                double price = 0.0;
                Console.WriteLine("Please give the price you want. And write it down like in the example (7.50)");
                while (true)
                {
                    try
                    {
                        string priceString = Console.ReadLine();
                        price = Convert.ToDouble(priceString);
                        if (price > 0.0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease enter a price above 0.00 (7.50)");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nThe price was not put in correctly. Please write it down like in the example (7.50)");
                    }
                }

                MySqlParameter priceParam = new MySqlParameter("@price", MySqlDbType.Double);
                MySqlParameter hallParam = new MySqlParameter("@hall", MySqlDbType.Int32);

                priceParam.Value = price;
                hallParam.Value = hall;

                if (circle == 1) 
                {
                    string StringtoUpdate = @"UPDATE prices SET InnerCircle = @price WHERE Hall = @hall";

                    MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);
                    
                    command.Parameters.Add(priceParam);
                    command.Parameters.Add(hallParam);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
                else if (circle == 2)
                {
                    string StringtoUpdate = @"UPDATE prices SET MiddleCircle = @price WHERE Hall = @hall";

                    MySqlCommand command = new MySqlCommand(StringtoUpdate, Connection);

                    command.Parameters.Add(priceParam);
                    command.Parameters.Add(hallParam);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
                else if (circle == 3)
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
    }
}