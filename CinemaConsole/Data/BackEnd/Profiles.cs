using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.BackEnd
{
    public class Profiles
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Function { get; set; }

        public Profiles(string username, string password, string function)
        {
            Username = username;
            Password = password;
            Function = function;
        }
    }
}
