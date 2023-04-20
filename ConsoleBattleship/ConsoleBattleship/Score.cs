using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ConsoleBattleship;




// need to see where this fits in and how it is called.

namespace ConsoleBattleship.Score
{
    public class Score
    {
        private int player1Score;
        private int player2Score;
        public bool isGameActive;
        // both of these need to be set when the game starts
        public string user1Name;
        public string user2Name;
        public string winningUsername;
        public string loosingUsername;

        private DbHandler databaseX = new DbHandler();

        public Score()
        {
            player1Score = 0;
            player2Score = 0;
            user1Name = "";
            user2Name = "";
            winningUsername = "";
            loosingUsername = "";
            // isGameActive here
        }

        public void setupGame(string name1, string name2)
        {
            user1Name = name1;
            user2Name = name2;
        }

        public void incrementPlayer1Score(int points)
        {
            player1Score = +points;
            if (detectWin(player1Score) == true)
            {
                winningUsername = user1Name;
                loosingUsername = user2Name;
                updateDatabase(winningUsername, loosingUsername);
            }
        }

        public void incrementPlayer2Score(int points)
        {
            player2Score = +points;
            if (detectWin(player1Score) == true)
            {
                winningUsername = user2Name;
                loosingUsername = user1Name;
                updateDatabase(winningUsername, loosingUsername);
            }
        }

        public int getPlayer1Score()
        {
            return player1Score;
        }

        public int getPlayer2Score()
        {
            return player2Score;
        }

        public void resetGame()
        {
            player1Score = 0;
            player2Score = 0;
            // game should be actuve when played, and be not when it ends or no game is played. 
            // This means isGameActive would need to be updated extenally when a new game is being set up.
            isGameActive = true;
        }

        public bool detectWin(int score)
        {
            if (score >= 17)
            {
                // need to check after every sunk ship is this is false, if so the game ends.
                isGameActive = false;
                return true;
            }
            return false;
        }

        public void displayScore()
        {
            // see how this fits in with the current game screen 
            Console.WriteLine("Player 1 score: " + player1Score + " | Player 2 score: " + player2Score);
        }

        private void updateDatabase(string winningUsername, string loosingUsername)
        {
            databaseX.updateUserWins(SQLiteConnection, winningUsername);
            databaseX.updateUserLosses(SQLiteConnection, loosingUsername);
        }

        public string displayLeaderboard()
        {
            //Console.WriteLine(database.getLeaderboard);
            return "";
        }

    }
}