using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data
{
    public class Profiles
    {
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
