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
    public class AdminData : Connecter
    {
        public void CreateSeat(int price, int Y, int X, int hall, bool avail, string name)
        {
            try
            {
                Connection.Open();

                string stringToInsert = @"INSERT INTO login (Price, RowSeat, ColumnSeat, Hall, Availability, Name) VALUES (@Price, @RowSeat, @ColumnSeat, @Hall, @Availability, @Name)";

                MySqlCommand command = new MySqlCommand(stringToInsert, Connection);
                MySqlParameter priceParam = new MySqlParameter("@Price", MySqlDbType.Int32);
                MySqlParameter RowSeatParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                MySqlParameter colSeatParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);
                MySqlParameter availParam = new MySqlParameter("@Availability", MySqlDbType.Bit);
                MySqlParameter NameParam = new MySqlParameter("@Name", MySqlDbType.VarChar);

                priceParam.Value = price;
                RowSeatParam.Value = Y;
                colSeatParam.Value = X;
                HallParam.Value = hall;
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

        public void EditAvail(int hall, int X, int Y, bool avail)
        {
            try
            {
                Connection.Open();

                string stringToUpdate = @"UPDATE seat SET Availability = @avail WHERE RowSeat = @RowSeat AND @ColumnSeat = @ColumnSeat AND Hall = @Hall";

                MySqlCommand command = new MySqlCommand(stringToUpdate, Connection);
                MySqlParameter RowSeatParam = new MySqlParameter("@RowSeat", MySqlDbType.Int32);
                MySqlParameter colSeatParam = new MySqlParameter("@ColumnSeat", MySqlDbType.Int32);
                MySqlParameter HallParam = new MySqlParameter("@Hall", MySqlDbType.Int32);
                MySqlParameter availParam = new MySqlParameter("@avail", MySqlDbType.Bit);

                RowSeatParam.Value = Y;
                colSeatParam.Value = X;
                HallParam.Value = hall;

                if (avail)
                {
                    availParam.Value = 0;
                }
                else
                {
                    availParam.Value = 1;
                }

                command.Parameters.Add(RowSeatParam);
                command.Parameters.Add(colSeatParam);
                command.Parameters.Add(HallParam);
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
    }
}
