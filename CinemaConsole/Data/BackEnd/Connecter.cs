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
            Builder.UserID = "phpmyadmin";
            Builder.Password = "admin";
            Builder.Server = "127.0.0.1";
            Builder.SshHostName = "145.24.222.66";
            Builder.SshUserName = "ubuntu-0969499";
            Builder.SshPassword = "885Zta";

            // string dbstring = "server=localhost;user=root;pwd=admin;database=cinema";
            Connection = new MySqlConnection(Builder.ConnectionString);
        }
    }
}
