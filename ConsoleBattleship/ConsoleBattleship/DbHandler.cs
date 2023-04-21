using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace ConsoleBattleship
{
    internal class DbHandler
    {

        private static DbHandler DBinstance = null;
        private static readonly object locker = new object(); //thread locker


        static string url = @"URI=file:battleship.db";
        SQLiteConnection connection = new SQLiteConnection(url);

        DbHandler()
        {

        }

        public static DbHandler Instance
        {
            get
            {
                if (DBinstance == null)
                {
                    lock (locker)
                    {
                        if (DBinstance == null)
                        {
                            DBinstance = new DbHandler();
                        }
                    }
                }
                return DBinstance;
            }
        }
        public string getAllUsers()
        {
            string output = "";

            string statement = "SELECT * FROM users";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();
            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}   {reader.GetInt32(2)}    {reader.GetInt32(3)}\n";
            }
            connection.Close();
            return output;
        }

        public bool isValidUser(string username, string password)
        {
            string output = "";

            string statement = $"SELECT * FROM users WHERE username =\"{username}\" AND password =\"{password}\"";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}    {reader.GetInt32(2)}   {reader.GetInt32(3)}\n";
            }

            connection.Close();

            if (output.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool doesUsernameExist(string username)
        {
            string output = "";

            string statement = $"SELECT * FROM users WHERE username =\"{username}\"";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}    {reader.GetInt32(2)}   {reader.GetInt32(3)}\n";
            }
            connection.Close();

            if (output.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getUserByUsername(string username)
        {
            string output = "";

            string statement = $"SELECT * FROM users WHERE username =\"{username}\"";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}    {reader.GetInt32(2)}   {reader.GetInt32(3)}\n";
            }
            connection.Close();
            return output;
        }

        public void createUser(string newUsername, string newPassword)
        {
            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = $"INSERT INTO users(username, password) VALUES(\"{newUsername}\",\"{newPassword}\")";
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void updateUserUsername(string oldUsername, string newUsername)
        {

            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = $"UPDATE users SET username = \"{newUsername}\" WHERE username = \"{oldUsername}\"";
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void updateUserPassword(string username, string newPassword)
        {

            using var cmd = new SQLiteCommand(connection);
            connection.Open();

            cmd.CommandText = $"UPDATE users SET password = \"{newPassword}\" WHERE username = \"{username}\"";
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void updateUserWins(string username)
        {
            string getWins = $"SELECT wins FROM users WHERE username = \"{username}\"";

            using var cmd = new SQLiteCommand(getWins, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            int wins = 0;

            while (reader.Read())
            {
                wins = reader.GetInt32(0);
            }

            using var cmdTwo = new SQLiteCommand(connection);

            cmdTwo.CommandText = $"UPDATE users SET wins = ({wins}+1) WHERE username = \"{username}\"";
            cmdTwo.ExecuteNonQuery();

            connection.Close();
        }

        public void updateUserLosses(string username)
        {
            string getLosses = $"SELECT losses FROM users WHERE username = \"{username}\"";

            using var cmd = new SQLiteCommand(getLosses, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            int losses = 0;

            while (reader.Read())
            {
                losses = reader.GetInt32(0);
            }

            using var cmdTwo = new SQLiteCommand(connection);

            cmdTwo.CommandText = $"UPDATE users SET losses = ({losses}+1) WHERE username = \"{username}\"";
            cmdTwo.ExecuteNonQuery();

            connection.Close();
        }

        public void createGame(string playerOneUsername, string playerTwoUsername, int playerOneScore, int playerTwoScore)
        {
            int previousId = 0;

            string statement = "SELECT game_id FROM games ORDER BY game_id DESC LIMIT 1";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                previousId = reader.GetInt32(0);
            }

            int newId = previousId + 1;

            string playerOneUserId = getUserByUsername(playerOneUsername);
            string playerTwoUserId = getUserByUsername(playerTwoUsername);
            playerOneUserId = playerOneUserId.Substring(0, playerOneUserId.IndexOf(" "));
            playerTwoUserId = playerTwoUserId.Substring(0, playerTwoUserId.IndexOf(" "));

            using var cmdTwo = new SQLiteCommand(connection);

            cmdTwo.CommandText = $"INSERT INTO games(game_id,user_id,score) VALUES({newId},{playerOneUserId},{playerOneScore}),({newId},{playerTwoUserId},{playerTwoScore})";
            cmdTwo.ExecuteNonQuery();

            connection.Close();

        }

        public string getAllGames()
        {
            string output = "";

            string statement = "SELECT * FROM games";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetInt32(1)}    {reader.GetInt32(2)}\n";
            }
            connection.Close();
            return output;
        }

        public string getLeaderboard()
        {
            string output = "Top 10 Leaderboard:\n";

            string statement = "SELECT users.username, games.score " +
                "FROM games " +
                "INNER JOIN users ON games.user_id = users.user_id " +
                "WHERE games.score > 0 " +
                "ORDER BY games.score ASC " +
                "LIMIT 10";

            using var cmd = new SQLiteCommand(statement, connection);
            connection.Open();

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetString(0)}    {reader.GetInt32(1)}\n";
            }

            connection.Close();
            return output;
        }
    }
}
