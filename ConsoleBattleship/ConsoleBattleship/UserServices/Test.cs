using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.UserServices
{
    class Test
    {
        static void Main(string[] args)
        {
            Authentication auth = new Authentication();

            while (true)
            {
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Log In");
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Forgot Password");
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        auth.SignUp();
                        break;
                    case "2":
                        auth.LogIn();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    case "4":
                        auth.ForgotPassword();
                        break;
                    default:
                        Console.WriteLine("********************Invalid choice. Please try again********************");
                        break;
                }
            }
        }
    }
}
