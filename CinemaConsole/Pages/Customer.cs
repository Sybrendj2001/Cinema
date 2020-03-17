using System;
using System.Collections.Generic;


namespace CinemaConsole.Pages
{

    public class Customer
    {
        public class Agenda : IEquatable<Agenda>
        {
            public string AgendaTime { get; set; }

            public string AgendaDate { get; set; }

            public int AgendaId { get; set; }

            public override string ToString()
            {
                return "ID: " + AgendaId + "   Datum: " + AgendaDate + "   Time: " + AgendaTime;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Agenda objAsAgenda = obj as Agenda;
                if (objAsAgenda == null) return false;
                else return Equals(objAsAgenda);
            }
            public override int GetHashCode()
            {
                return AgendaId;
            }
            public bool Equals(Agenda other)
            {
                if (other == null) return false;
                return (this.AgendaId.Equals(other.AgendaId));
            }
        }
        public class Example
        {
            public static void Main()
            {
                // Create a list of times.
                List<Agenda> agenda = new List<Agenda>();

                // Add Time and Dates to the list.
                agenda.Add(new Agenda { AgendaTime = "12:30", AgendaDate = "12-06-2020", AgendaId = 1 });
                agenda.Add(new Agenda { AgendaTime = "19:45", AgendaDate = "12-06-2020", AgendaId = 2 });
                agenda.Add(new Agenda { AgendaTime = "18:00", AgendaDate = "13-06-2020", AgendaId = 3 });
                agenda.Add(new Agenda { AgendaTime = "21:30", AgendaDate = "13-06-2020", AgendaId = 4 });
                agenda.Add(new Agenda { AgendaTime = "15:30", AgendaDate = "14-06-2020", AgendaId = 5 });
                agenda.Add(new Agenda { AgendaTime = "21:00", AgendaDate = "14-06-2020", AgendaId = 6 });

                // Write out the time and dates in the list. This will call the overridden ToString method
                // in the Agenda class.
                Console.WriteLine();
                foreach (Agenda aAgenda in agenda)
                {
                    Console.WriteLine(aAgenda);
                }
                Console.WriteLine(" ");

                Console.WriteLine("Please enter yout choice. Type in ID: ");
                string CustomerTimeDate = Console.ReadLine();




            }
        }
    }
}