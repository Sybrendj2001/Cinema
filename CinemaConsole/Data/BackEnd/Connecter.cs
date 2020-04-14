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

        protected Connecter()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            string dbstring = "server=localhost;user=root;pwd=mini-cooper;database=cinema";
            Connection = new MySqlConnection(dbstring);
        }
    }
}