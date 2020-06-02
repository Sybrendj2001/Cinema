using System;
using System.Text;
using System.Security.Cryptography;
using CinemaConsole.Data.BackEnd;

namespace CinemaConsole.Pages
{
    public class Login
    {
        /// <summary>The function of the logged-in person</summary>
        public string Function = "";

        /// <summary>value of if you are logged-in or not</summary>
        public bool loggedIn = false;

        /// <summary>
        /// Creates a hashed value
        /// </summary>
        /// <param name="valueToEncrypt">A unhashed string</param>
        /// <returns>a hashed string</returns>
        public static string sha256_hash(string valueToEncrypt)
        {
            StringBuilder stringbuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(valueToEncrypt));

                foreach(byte b in result)
                {
                    stringbuilder.Append(b.ToString());
                }
            }
            return stringbuilder.ToString();
        }

        /// <summary>
        /// Menu for the login
        /// </summary>
        public void Menu()
        {
            ShowData SD = new ShowData();
            bool checkLogin = true;
            Console.Clear();
            SHA256 encrypter = SHA256.Create();
            while (checkLogin == true)
            {
                Console.WriteLine("\nGive your Username[CaseSenSiTive] or enter [exit] to return to the menu");
                string Username = Console.ReadLine();
                if (Username == "exit")
                {
                    break;
                }
                else if (Username.Contains(" "))
                {
                    Console.WriteLine("You are not using the right format!");
                }
                else
                {
                    Console.WriteLine("Fill in your Password[CaseSenSiTive] or enter [exit] to return to the menu");
                    string Password = Console.ReadLine();
                    if (Password == "exit")
                    {
                        break;
                    }
                    else if (Username.Contains(" "))
                    {
                        Console.WriteLine("You are not using the right format!");
                    }
                    else
                    {
                        ChangeData logincheck = new ChangeData();

                        string encryptedusername = sha256_hash(Username);
                        string encryptedpassword = sha256_hash(Password);

                        string checkeddata = logincheck.checkLoginAndFunction(encryptedusername, encryptedpassword);

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
}