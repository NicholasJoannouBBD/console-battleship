using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleBattleship.UserServices
{
    class Authentication
    {
        DbHandler handler = DbHandler.Instance;
        private static List<string> menuItems = new List<string> {"Find Game", "Profile", "High scores"};
        Menu menu = new Menu(menuItems);
        
        

        public void SignUp()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            //Check if user already exists
            bool isValid = handler.isValidUser(username, password);
            if (isValid)
            {
                Console.WriteLine();
                Console.WriteLine("********************Username already exists. Please pick a different username.********************");
                Console.WriteLine();
                return;
            }

            //Add new user to DB
            handler.createUser(username, password);
            bool isCreated = handler.isValidUser(username, password);
            if (isCreated)
            {
                Console.WriteLine();
                Console.WriteLine("********************Sign up successful. Please log in.********************");
                Console.WriteLine();
                LogIn();
                //return;
            }

        }

        public void LogIn()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            //Verify username and password
            bool isValid = handler.isValidUser(username, password);
            if (isValid)
            {
                Console.WriteLine();
                Console.WriteLine("********************Login successful.********************");
                Console.WriteLine();
                menu.displayMenu();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("********************Invalid Username or Password. Please try again.********************");
                Console.WriteLine();
            }
        }

        public void ForgotPassword()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter New Password: ");
            string password = Console.ReadLine();

            //Verify username and password
            bool userExists = handler.doesUsernameExist(username);
                
            if (userExists)
            {
                handler.updateUserPassword(username, password);
                Console.WriteLine();
                Console.WriteLine("********************Password updated successfully. Please log in!********************");
                Console.WriteLine();
                LogIn();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("********************Password update failed. Please try again.********************");
                Console.WriteLine();
            }
            
        }
    }
}
