using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace CinemaConsole.Data.BackEnd
{
    public abstract class Connecter
    {
        protected MySqlConnection Connection;

        MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();


        protected Connecter()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.UserID = "root";
            Builder.Password = "admin";
            Builder.Server = "127.0.0.1";
            Builder.SshHostName = "145.24.222.66";
            Builder.SshHostName = "mysql";
            Builder.SshPassphrase = "mysql";
            Builder.Database = "cinema";
            Builder.Port = 3306;
            Builder.SshPort = 12886;
            Connection = new MySqlConnection(Builder.ConnectionString);
        }
    }
}