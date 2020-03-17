using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaConsole.Pages;

namespace CinemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string pageTobe = "MainMenu";
            bool start = false;
            while(start == false){
                Console.WriteLine("Do you want to see the movielist or login? (movielist/login)");
                string toDo = Console.ReadLine();
                if(toDo == "Login"){
                    bool checkLogin = true;
                    while(checkLogin == true){
                        Console.WriteLine("Give your credentials:(username - password)");
                        string login = Console.ReadLine();
                        string[] credentials = login.Split(' ');
                        if (credentials.Length != 2){
                            Console.WriteLine("Your credentials are not in the right format. (username - password)");
                        }
                        else{
                            Login log = new Login(credentials[0],credentials[1]);
                            checkLogin = false;
                            pageTobe = log.Function;
                            Console.WriteLine("You are logged in");
                        }
                    }
                }
                else if (toDo == "Movielist")
                {
                    Console.WriteLine("This function is not here");
                }
                else if (toDo == "Help")
                {
                    Console.WriteLine("Help: show help.\nLogin: Log into your own page.\nMovielist: Show movielist.");
                }
                else
                {
                    Console.WriteLine("This function doesnt exist yet");
                }
            }
        }
    }
}
