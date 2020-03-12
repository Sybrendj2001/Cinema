using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Pages
{
    public class Employee
    {
        private string Password { get; set; }

        private string Username { get; set; }

        private void ChangeUsername(string currentUsername, string newUsername)
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

        private void changePassword(string currentPassword, string newPassword)
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
