using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data
{
    public class Profiles
    {
        public Dictionary<string, string> logincredentials = new Dictionary<string, string>()
        {
            { "sybrendj" , "1234"},
            { "mark010" , "4567"},
            { "timf" , "7891"}

        };

        public Dictionary<string, string> usernameStatus = new Dictionary<string, string>()
        {
            { "sybrendj" , "admin" },
            { "mark010" , "retailer"},
            { "timf" , "ticketsalesman"}
        };

        public bool dictsAreSame = false;

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Function { get; set; }


        public Profiles(string username, string password, string function)
        {
            UserName = username;
            Password = password;
            Function = function;
        }
    }
}
