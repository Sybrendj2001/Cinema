using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
    public class Movies
    {
        private int MovieId { get;}

        private string MovieName { get; set; }

        private string MovieDuration { get; set; }

        public Movies(int id, string name, string duration) 
        {
            MovieId = id;
            MovieName = name;
            MovieDuration = duration;
        }
    }
}
