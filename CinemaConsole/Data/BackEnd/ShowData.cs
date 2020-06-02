using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using CinemaConsole.Pages;


namespace CinemaConsole.Data.BackEnd
{
    public class ShowData : Connecter
    {
        /// <summary>
        /// show all movies from the db
        /// </summary>
        public List<int> ShowMovies()
        {
            try
            {
                List<int> MovieIDs = new List<int>();
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
                        MovieIDs.Add(Convert.ToInt32(row["MovieID"]));
                        movieID = row["MovieID"].ToString();
                        movieName = row["MovieName"].ToString();
                        movieYear = row["MovieYear"].ToString();
                        Console.WriteLine("[" + movieID + "] " + movieName + " (" + movieYear + ")");
                    }
                    return MovieIDs;
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
        /// <param name="MovieID">given movie id</param>
        public Tuple<string,string,string> ShowMovieByID(string MovieID)
        {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie WHERE MovieID = @id";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);
                oCmd.Parameters.AddWithValue("@id", MovieID);

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getMovieInfo);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.Clear();
                        Console.WriteLine("\nMovie selected: " + row["MovieName"].ToString());
                        Console.WriteLine("Year: " + row["MovieYear"].ToString());
                        Console.WriteLine("Age restriction: " + row["MovieMinimumAge"].ToString() + "+");
                        Console.WriteLine("Actors: " + row["MovieActors"].ToString());
                        Console.WriteLine("Summary: " + row["MovieSummary"].ToString());

                        // show the times with the id of the movie
                        return Tuple.Create(row["MovieID"].ToString(), row["MovieName"].ToString(), row["MovieMinimumAge"].ToString());
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
            return Tuple.Create("","","");
        }

        /// <summary>
        /// Show the extra movie info with the right ID
        /// </summary>
        /// <param name="MovieID">given movie id</param>
        /// <param name="Option">select 1 = title, 2 = year, 3 = age, 4 = actors, 5 = summary</param>
        public void ShowMovieInfoPartlyByID(string MovieID, int Option)
        {
            try
            {
                Connection.Open();
                string oString = @"SELECT * from movie WHERE MovieID = @id";
                MySqlCommand oCmd = new MySqlCommand(oString, Connection);
                oCmd.Parameters.AddWithValue("@id", MovieID);

                using (MySqlDataReader getMovieInfo = oCmd.ExecuteReader())
                {
                    while (getMovieInfo.Read())
                    {
                        switch(Option)
                        {
                            case 1:
                                Console.WriteLine("\nCurrent movie title: " + getMovieInfo["MovieName"].ToString());
                                break;
                            case 2:
                                Console.WriteLine("\nCurrent year: " + getMovieInfo["MovieYear"].ToString());
                                break;
                            case 3:
                                Console.WriteLine("\nCurrent age restriction: " + getMovieInfo["MovieMinimumAge"].ToString());
                                break;
                            case 4:
                                Console.WriteLine("\nCurrent actors: " + getMovieInfo["MovieActors"].ToString());
                                break;
                            case 5:
                                Console.WriteLine("\nCurrent summary: " + getMovieInfo["MovieSummary"].ToString());
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
        /// Search funtion ticketsalesman. Search on name, search on ticketnumber and surch on movie name and date/time
        /// </summary>
        public void DisplayTickets()
        {
            ShowData SD = new ShowData();
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Connection.Open();
                string TicketInfo = @"SELECT * FROM ticket";
                string MovieInfo = @"SELECT * FROM movie";
                string DateInfo = @"SELECT * FROM date";

                MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);
                MySqlCommand oCmd2 = new MySqlCommand(MovieInfo, Connection);
                MySqlCommand oCmd3 = new MySqlCommand(DateInfo, Connection);

                // creating the strings 
                string TicketID;
                string TicketCode;
                string Owner;
                string Email;
                string MovieID;
                string DateID;

                bool isFound;
                int amountofticketscounted;
                int amountoftickets;

                using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getTicketInfo);
                    Console.Clear();
                    bool k = true;

                    // menu of the three search options
                    while (k)
                    {
                        isFound = false;
                        amountofticketscounted = 0;
                        amountoftickets = dataTable.Rows.Count;
                        Console.WriteLine("\n[1] Search on name\n[2] Search on ticket number\n[3] Search using customer's emailaddress\n[4] Search on movie, time and date\n[exit] To go back to the menu");
                        string SearchOption = Console.ReadLine();
                        if (SearchOption.Length > 5)
                        {
                            ClearAndErrorMessage("Your input is to big");
                        }
                        else if (SearchOption == "1")
                        {
                            Console.Clear();
                            Console.WriteLine("\nPlease enter the customer full name or enter [exit] to exit");

                            string name2 = Console.ReadLine();
                            string name = name2.ToString().ToLower();

                            while (true)
                            {
                                Console.Clear();
                                if (name2 == "exit")
                                {
                                    // using k to break out of the outer loop
                                    k = false;
                                    break;
                                }

                                // going through the data
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    amountofticketscounted += 1;
                                    Owner = row["Owner"].ToString();
                                    TicketCode = row["TicketCode"].ToString();
                                    TicketID = row["TicketID"].ToString();
                                    MovieID = row["MovieID"].ToString();
                                    DateID = row["DateID"].ToString();

                                    // check if there is a match
                                    if (Owner == name)
                                    {
                                        isFound = true;
                                        Connection.Close();

                                        // going to the overview with all the details
                                        Console.WriteLine("\nTicket [" + TicketID + "]");
                                        Overview(TicketID, MovieID, DateID);
                                    }
                                }

                                // check if all tickets were checked
                                if (amountoftickets == amountofticketscounted)
                                {
                                    if (isFound)
                                    {
                                        Console.WriteLine("\nPress enter to continue");
                                        k = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere were no results found with the name: " + name + "\nPress enter to go back to the menu");
                                    }
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                        }

                        else if (SearchOption == "2")
                        {
                            Console.Clear();

                            while (true)
                            {
                                Console.WriteLine("\nPlease enter the ticketnumber or enter [exit] to go back to the menu");
                                string ticketnumber = Console.ReadLine();
                                if (ticketnumber == "exit")
                                {
                                    Console.Clear();
                                    // using k to break out of the outer loop
                                    k = false;
                                    break;
                                }

                                // going through the data
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    amountofticketscounted += 1;
                                    Owner = row["Owner"].ToString();
                                    TicketCode = row["TicketCode"].ToString();
                                    TicketID = row["TicketID"].ToString();
                                    MovieID = row["MovieID"].ToString();
                                    DateID = row["DateID"].ToString();

                                    // check if there is a match
                                    if (TicketCode == ticketnumber)
                                    {
                                        isFound = true;
                                        Connection.Close();

                                        // going to the overview with all the details
                                        Console.Clear();
                                        Overview(TicketID, MovieID, DateID);
                                        Console.WriteLine("\nPress enter to continue");
                                        Console.ReadLine();
                                        break;
                                    }
                                }

                                // check if all tickets were checked
                                if (amountoftickets == amountofticketscounted)
                                {
                                    if (isFound)
                                    {
                                        Console.WriteLine("\nPress enter to continue");
                                        k = false;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nThere were no results found with ticketnumber: " + ticketnumber + " Please enter to continue");
                                    }
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                        else if (SearchOption == "3")
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("\nPlease enter the customer's emailaddress or enter [exit] to go back");
                                string emailaddress = Console.ReadLine();
                                Console.Clear();
                                if (emailaddress == "exit")
                                {
                                    // using k to break out of the outer loop
                                    k = false;
                                    break;
                                }

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    amountofticketscounted += 1;
                                    Email = row["Email"].ToString();
                                    TicketID = row["TicketID"].ToString();
                                    MovieID = row["MovieID"].ToString();
                                    DateID = row["DateID"].ToString();
                                    if (Email == emailaddress)
                                    {
                                        Connection.Close();
                                        // Ticket and contact information overview
                                        Console.WriteLine("\nTicket [" + TicketID + "]");
                                        Overview(TicketID, MovieID, DateID);
                                        isFound = true;
                                    }
                                }

                                // check if all tickets were checked
                                if (amountoftickets == amountofticketscounted)
                                {
                                    if (isFound)
                                    {
                                        Console.WriteLine("\nPress enter to continue");
                                        k = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThere were no results found with email: " + emailaddress + "\nPress enter to go back to the menu");
                                    }
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                }
                            }
                        }

                        else if (SearchOption == "4")
                        {
                            Console.Clear();
                            isFound = false;
                            Connection.Close();
                            ShowMovies();
                            string movie = "";

                            Connection.Open();

                            using (MySqlDataReader getMovieInfo = oCmd2.ExecuteReader())
                            {
                                DataTable dataTable2 = new DataTable();
                                dataTable2.Load(getMovieInfo);

                                string MovieName;
                                isFound = true;

                                while (isFound)
                                {
                                    Console.WriteLine("\nPlease enter the movie");
                                    movie = Console.ReadLine();
                                    foreach (DataRow row in dataTable2.Rows)
                                    {
                                        MovieName = row["MovieID"].ToString();

                                        if (movie == MovieName)
                                        {
                                            isFound = false;
                                            break;
                                        }
                                    }
                                    if (isFound == true)
                                    {
                                        ErrorMessage("Your input was too big");

                                    }
                                }
                            }

                            Connection.Close();

                            Console.Clear();

                            Tuple<List<DateTime>, List<int>, List<int>> dates = Customer.showTime(movie);
                            string SelectedTime = Customer.selectTime(dates, movie);
                            if (SelectedTime == "exit")
                            {
                                break;
                            }

                            int movieid = Convert.ToInt32(movie);

                            AdminData AD = new AdminData();

                            Tuple<List<DateTime>, List<int>, List<int>> times = AD.GetTime(Convert.ToInt32(movie));

                            int GetDateID = times.Item2[0];

                            Connection.Open();

                            MySqlDataReader getDateInfo = oCmd3.ExecuteReader();
                            DataTable dataTable3 = new DataTable();

                            dataTable3.Load(getDateInfo);

                            Console.Clear();
                            while (true)
                            {
                                // going through ticket data
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    TicketID = row["TicketID"].ToString();
                                    MovieID = row["MovieID"].ToString();
                                    DateID = row["DateID"].ToString();

                                    // going through all the ticket data to see if there is a match between all the given information
                                    if (movieid == Convert.ToInt32(row["MovieID"]) && GetDateID == Convert.ToInt32(row["DateID"]))
                                    {
                                        isFound = true;
                                        Connection.Close();

                                        // going to the overview with all the details
                                        Overview(TicketID, MovieID, DateID);

                                        // using k to break out of the outer loop
                                        k = false;
                                    }
                                }

                                if (isFound)
                                {
                                    Console.WriteLine("\nPress enter to go back to the menu");
                                    string exit = Console.ReadLine();
                                    // using k to break out of the outer loop
                                    k = false;
                                    break;
                                }

                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nThere were no results found. Press enter to go back to the menu");
                                    string exit = Console.ReadLine();
                                    Console.Clear();
                                    // using k to break out of the outer loop
                                    k = false;
                                    break;
                                }
                            }
                        }

                        else if (SearchOption == "exit")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                            SD.ClearAndErrorMessage("Your input is too big");
                    }
                }
            }

            catch (MySqlException ex)
            {
                ErrorMessage("Your input was too big");
            }
            finally
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// Overview of all the information about the customer and the movie they reserved.
        /// </summary>
        /// <param name="TicketID">The ID of the ticket</param>
        /// <param name="MovieID">The ID of the movie</param>
        /// <param name="DateID">The ID of the date</param>
        public void Overview(string TicketID, string MovieID, string DateID)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Connection.Open();
                string TicketInfo = @"SELECT * FROM ticket";
                string MovieInfo = @"SELECT * FROM movie";
                string DateInfo = @"SELECT * FROM date";

                MySqlCommand oCmd = new MySqlCommand(TicketInfo, Connection);
                MySqlCommand oCmd2 = new MySqlCommand(MovieInfo, Connection);
                MySqlCommand oCmd3 = new MySqlCommand(DateInfo, Connection);

                // creating the strings 
                string movieTitle;
                string movieYear;
                string Owner;
                string Email;
                string TicketCode;
                int SeatX;
                int SeatY;
                int amount;
                string Datetime;
                string Hall;
                double TotalPrice;

                using (MySqlDataReader getMovieInfo = oCmd2.ExecuteReader())
                {
                    DataTable dataTable2 = new DataTable();

                    dataTable2.Load(getMovieInfo);

                    // going through movie data
                    foreach (DataRow row in dataTable2.Rows)
                    {
                        if (MovieID == row["MovieID"].ToString())
                        {
                            movieTitle = row["MovieName"].ToString();
                            movieYear = row["MovieYear"].ToString();
                            Console.WriteLine("\n" + movieTitle + "   " + movieYear);
                        }
                    }
                }

                using (MySqlDataReader getDateTimeHallInfo = oCmd3.ExecuteReader())
                {
                    DataTable dataTable3 = new DataTable();

                    dataTable3.Load(getDateTimeHallInfo);

                    // going through date data
                    foreach (DataRow row in dataTable3.Rows)
                    {
                        if (DateID == row["DateID"].ToString())
                        {
                            Datetime = Convert.ToDateTime(row["DateTime"]).ToString("dd/MM/yyyy HH:mm");
                            Hall = row["Hall"].ToString();

                            Console.WriteLine(Datetime + "   Hall: " + Hall);
                        }
                    }
                }

                using (MySqlDataReader getTicketInfo = oCmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Load(getTicketInfo);

                    // going through ticket data
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (TicketID == row["TicketID"].ToString())
                        {
                            Owner = row["Owner"].ToString();
                            Email = row["Email"].ToString();
                            TicketCode = row["TicketCode"].ToString();
                            TotalPrice = Convert.ToDouble(row["TotalPrice"], System.Globalization.CultureInfo.InvariantCulture);

                            SeatX = Convert.ToInt32(row["seatX"]);
                            SeatY = Convert.ToInt32(row["seatY"]);
                            amount = Convert.ToInt32(row["amount"]);

                            string seats = "";

                            for (int i = SeatX; i < amount + SeatX; i++)
                            {
                                seats += "(" + i + "/" + SeatY + ") ";
                            }

                            Console.WriteLine("Seats: " + seats);

                            Console.WriteLine(Owner + "    " + Email + "\nTicket number: " + TicketCode);
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
        /// Write a message in an error state
        /// </summary>
        /// <param name="message">The message you want to write</param>
        public void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Write a message in an error state and clear before you write the error message
        /// </summary>
        /// <param name="message">The message you want to write</param>
        public void ClearAndErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
