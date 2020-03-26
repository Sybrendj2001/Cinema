using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CinemaConsole.Pages
{
    public class Employee
    {
        public string Password { get; set; }

        public string Username { get; set; }

        public void ChangeUsername(string currentUsername, string newUsername)
        {
            if (currentUsername == Username)
            {
                Username = newUsername;
            }
            else
            {
                Username = Username;
            }
        }

        public void changePassword(string currentPassword, string newPassword)
        {
            if (currentPassword == Password)
            {
                Username = newPassword;
            }
            else
            {
                Password = Password;
            }
        }
    }
}
