using System;
namespace ConsoleBattleship
{
	public partial class Score
	{
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

        public void resetGame()
        {
            player1Score = 0;
            player2Score = 0;
            isGameActive = true;
        }
    }
}

