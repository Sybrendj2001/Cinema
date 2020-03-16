using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
    public class Movies
    {
        private string MovieName {get; set;}

        private string MovieDuration {get; set;}

        private int MovieId {get; }

        public Movies(string name, string duration, int id){
            name = MovieName;
            duration = MovieDuration;
            id = MovieId;
        }
    }
}
