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
            Mname = name;
            Myear = year;
            Mage = age;
            Msumm = summary;
            Mactors = actors;
        }

        public Tuple<int, string, int, int, string, string> getMovieInfo()
        {
            int idd = Mid;
            string name = Mname;
            int year = Myear;
            int age = Mage;
            string summary = Msumm;
            string actors = Mactors;

            return Tuple.Create(idd, name, year, age, summary, actors);
        }
    }
}
