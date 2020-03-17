using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
    public class Movies
    {
        private int Mid { get; set; }

        private string Mname { get; set; }

        private int Myear { get; set; }

        private int Mage { get; set; }

        private string Msumm { get; set; }

        private string Mactors { get; set; }

        public Movies(int id, string name, int year, int age, string summary, string actors)
        {
            Mid = id;
            Myear = year;
            Mname = name;
            Mage = age;
            Msumm = summary;
            Mactors = actors;
        }

        public Tuple<int, string, int> getMovieInfo()
        {
            int idd = Mid;
            string name = Mname;
            int year = Myear;

            return Tuple.Create(idd, name, year);
        }
    }
}
