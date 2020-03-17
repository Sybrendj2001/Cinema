using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages.Admin;
using CinemaConsole.Pages.Customer;

namespace CinemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Just to add 3 basic movies
            Customer.AddStuff();

            // running the customer menu
            Customer.Menu();
        }
    }
}
