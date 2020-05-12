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
        private string Username { get; set; }

        private string Password { get; set; }

        public string Function = "";

        public bool loggedIn = false;

        public Login()
        {

        }

        public void Menu()
        {
            ShowData SD = new ShowData();
            bool checkLogin = true;
            Console.Clear();
            while (checkLogin == true)
            {
                Console.WriteLine("\nGive your credentials: [username<space>password] or enter [exit] to return to the menu");
                string login = Console.ReadLine();
                if (login == "exit")
                {
                    break;
                }
                string[] credentials = login.Split(' ');
                if (credentials.Length != 2)
                {
                    SD.ClearAndErrorMessage("\nYour credentials are not in the right format. [username<space>password]");
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
                        SD.ClearAndErrorMessage("\nWrong Username/Password");
                    }
                }
            }

        }
    }
}