using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsoleBattleship
{
    internal class DbHandler
    {
        public string getAllUsers(SQLiteConnection con)
        {
            string output = "";

            string statement = "SELECT * FROM users";

            using var cmd = new SQLiteCommand(statement, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}   {reader.GetInt32(2)}    {reader.GetInt32(3)}\n";
            }
            return output;
        }

        public string getUserByUsername(SQLiteConnection con, string username)
        {
            string output = "";

            string statement = $"SELECT * FROM users WHERE username =\"{username}\"";

            using var cmd = new SQLiteCommand(statement, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetString(1)}    {reader.GetInt32(2)}   {reader.GetInt32(3)}\n";
            }
            return output;
        }

        public void createUser(SQLiteConnection con, string newUsername)
        {
            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = $"INSERT INTO users(username) VALUES(\"{newUsername}\")";
            cmd.ExecuteNonQuery();
        }

        public void updateUserWins(SQLiteConnection con, string username)
        {
            string getWins = $"SELECT wins FROM users WHERE username = \"{username}\"";

            using var cmd = new SQLiteCommand(getWins, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            int wins = 0;

            while (reader.Read())
            {
                wins = reader.GetInt32(0);
            }

            using var cmdTwo = new SQLiteCommand(con);

            cmdTwo.CommandText = $"UPDATE users SET wins = ({wins}+1) WHERE username = \"{username}\"";
            cmdTwo.ExecuteNonQuery();
        }

        public void updateUserLosses(SQLiteConnection con, string username)
        {
            string getLosses = $"SELECT losses FROM users WHERE username = \"{username}\"";

            using var cmd = new SQLiteCommand(getLosses, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            int losses = 0;

            while (reader.Read())
            {
                losses = reader.GetInt32(0);
            }

            using var cmdTwo = new SQLiteCommand(con);

            cmdTwo.CommandText = $"UPDATE users SET losses = ({losses}+1) WHERE username = \"{username}\"";
            cmdTwo.ExecuteNonQuery();
        }

        public void createGame(SQLiteConnection con, string playerOneUsername, string playerTwoUsername, int playerOneScore, int playerTwoScore)
        {
            int previousId = 0;

            string statement = "SELECT game_id FROM games ORDER BY game_id DESC LIMIT 1";

            using var cmd = new SQLiteCommand(statement, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                previousId = reader.GetInt32(0);
            }

            int newId = previousId + 1;

            string playerOneUserId = getUserByUsername(con, playerOneUsername);
            string playerTwoUserId = getUserByUsername(con, playerTwoUsername);
            playerOneUserId = playerOneUserId.Substring(0, playerOneUserId.IndexOf(" "));
            playerTwoUserId = playerTwoUserId.Substring(0, playerTwoUserId.IndexOf(" "));

            using var cmdTwo = new SQLiteCommand(con);

            cmdTwo.CommandText = $"INSERT INTO games(game_id,user_id,score) VALUES({newId},{playerOneUserId},{playerOneScore}),({newId},{playerTwoUserId},{playerTwoScore})";
            cmdTwo.ExecuteNonQuery();

        }

        public string getAllGames(SQLiteConnection con)
        {
            string output = "";

            string statement = "SELECT * FROM games";

            using var cmd = new SQLiteCommand(statement, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetInt32(0)}    {reader.GetInt32(1)}    {reader.GetInt32(2)}\n";
            }
            return output;
        }

        public string getLeaderboard(SQLiteConnection con)
        {
            string output = "Top 10 Leaderboard:\n";

            string statement = "SELECT users.username, games.score " +
                "FROM games " +
                "INNER JOIN users ON games.user_id = users.user_id " +
                "WHERE games.score > 0 " +
                "ORDER BY games.score DESC " +
                "LIMIT 10";

            using var cmd = new SQLiteCommand(statement, con);

            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output += $"{reader.GetString(0)}    {reader.GetInt32(1)}\n";
            }
            return output;
        }
    }
}
