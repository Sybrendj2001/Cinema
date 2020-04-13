using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Data;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Pages
{
    public class Login
    {
        List<Profiles> profiles = new List<Profiles>()
        {
            {new Profiles("retailer","retailer","retailer") },
            {new Profiles("admin","admin","admin") },
            {new Profiles("retailer","retailer","retailer") },
            {new Profiles("ticketsalesman","ticketsalesman","ticketSalesman") },
            {new Profiles("admin","admin","admin") }
        };

        private string Username { get; set; }

        private string Password { get; set; }

        private int ErrorCode = 0;

        private int ProfilePlace = 0;

        public string ErrorMessage;

        public string Function = "";

        public bool loggedIn = false;

        public Login()
        {

        }

        private void register(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Menu()
        {
            bool checkLogin = true;
            while (checkLogin == true)
            {
                Console.WriteLine("Give your credentials:(username - password) or enter [exit] to return to the menu");
                string login = Console.ReadLine();
                if (login == "exit")
                {
                    break;
                }
                string[] credentials = login.Split(' ');
                if (credentials.Length != 2)
                {
                    Console.WriteLine("Your credentials are not in the right format. (username - password)");
                }
                else
                {
                    ChangeData logincheck = new ChangeData();
                    string checkeddata = logincheck.checkLoginAndFunction(credentials[0], credentials[1]);
                    if (checkeddata != "")
                    {
                        Function = checkeddata;
                        checkLogin = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Username/Password");
                    }
                }
            }

        }

        private string getErrorMessage(int id)
        {
            switch (id)
            {
                case 0:
                    return "There is nothing wrong";
                case 1:
                    return "Wrong Username or Password. \n Check for misspels";
                default:
                    return "There is nothing wrong";
            }
        }
    }
}