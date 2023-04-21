using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship;


namespace ConsoleBattleship
{
    public class Score
    {
        private int player1Score;
        private int player2Score;
        public bool isGameActive;
        public string user1Name;
        public string user2Name;
        public string winningUsername;
        public string loosingUsername;

        private DbHandler databaseX = DbHandler.Instance;

        public Score()
        {
            player1Score = 0;
            player2Score = 0;
            user1Name = "";
            user2Name = "";
            winningUsername = "";
            loosingUsername = "";
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
            isGameActive = true;
        }

        public bool detectWin(int score)
        {
            if (score >= 17)
            {
                isGameActive = false;
                return true;
            }
            return false;
        }

        public void displayScore()
        {
            // basic
            Console.WriteLine("Player 1 score: " + player1Score + " | Player 2 score: " + player2Score);

            // box

            //Console.WriteLine(" __________________________________________________________ ");
            //Console.WriteLine("|                                                          |");
            //Console.WriteLine("| Player 1 score: " + "17" + "           | Player 2 score: " + "12" + "        |");
            //Console.WriteLine("-----------------------------------------------------------");
        }

        private void updateDatabase(string winningUsername, string loosingUsername)
        {
            databaseX.updateUserWins(winningUsername);
            databaseX.updateUserLosses(loosingUsername);
        }

        public void displayLeaderboard()
        {
            Console.WriteLine(databaseX.getLeaderboard);
        }

    }
}