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
        /// <summary>Connection that can only be inherited so you cannot blatently open the database</summary>
        protected MySqlConnection Connection;

        /// <summary>
        /// Initializes the connection
        /// </summary>
        protected Connecter()
        {
            Initialize();
        }
        
        /// <summary>
        /// Initiliazes all secret components
        /// </summary>
        private void Initialize()
        {
            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.UserID = "cloginv3";
            Builder.Password = "Coockiedough";
            Builder.Server = "145.24.222.149";
            Builder.Database = "Cinema";
            Builder.Port = 3306;
            Connection = new MySqlConnection(Builder.ConnectionString);
            
        }
    }
}