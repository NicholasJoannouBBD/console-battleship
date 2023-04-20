using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.UserServices
{
    class Authentication
    {
        
        private string connectionString = "Data Source=TSEPOG\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;MultipleActiveResultSets=True";

        public void SignUp()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            //Check if user already exists
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("********************Username already exists. Please pick a different username.********************");
                    Console.WriteLine();
                    return;
                }
            }

            //Add new user to DB
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password) Values (@Username, @Password)";
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("********************Sign up successful. Please log in.********************");
                    Console.WriteLine();
                    return;
                }
            }
        }

        public void LogIn()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            //Verify username and password
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("********************Login successful.********************");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("********************Invalid Username or Password. Please try again.********************");
                    Console.WriteLine();
                }
            }
        }

        public void ForgotPassword()
        {
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter New Password: ");
            string password = Console.ReadLine();

            //Verify username and password
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET Password = @Password WHERE Username = @Username";
                SqlCommand cmd = new(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("********************Password updated successfully. Please log in!********************");
                    Console.WriteLine();
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
}
