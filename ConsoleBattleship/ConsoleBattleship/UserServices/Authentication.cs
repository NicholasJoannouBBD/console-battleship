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

        public void SignUp(string username, string password)
        {

            //Check if user already exists
            bool isValid = handler.doesUsernameExist(username);
            if (!isValid)
            {
                //Add new user to DB
                handler.createUser(username, password);
            }
            
        }

        public void LogIn(string user, string pwd)
        {
            string username = user;
            string password = pwd;

            //Verify username and password
            bool isValid = handler.isValidUser(username, password);
        }

        public void ForgotPassword(string username, string password)
        {

            //Verify username and password
            bool userExists = handler.doesUsernameExist(username);
                
            if (userExists)
            {
                handler.updateUserPassword(username, password);
            }
            
        }
    }
}
