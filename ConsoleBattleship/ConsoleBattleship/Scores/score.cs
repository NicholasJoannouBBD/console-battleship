using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    // need to see where this fits in and how it is called.

namespace ConsoleBattleship.Score
{
    public class Score {
    private int player1Score;
    private int player2Score;
    public bool isGameActive;
    
    public Score()
    {
        player1Score = 0;
        player2Score = 0;
        // isGameActive here
    }
    
    public void incrementPlayer1Score(int points)
    {
        player1Score =+ points;
        if(this.detectWin(player1Score) == true){
            // updateLeaderboard();
        }
    }
    
    public void incrementPlayer2Score(int points)
    {
        player2Score =+ points;
        if(this.detectWin(player1Score) == true){
            // updateLeaderboard();
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
        // game should be actuve when played, and be not when it ends or no game ic played. 
        // This means isGameActive would need to be updated extenally when a new game is being set up.
        isGameActive = true;
    }

    public bool detectWin(int score){
        if (score >= 17){
            // need to check after every sunk ship is this is false, if so the game ends.
            isGameActive = false;
            return true;
        } 
        return false;
    }

    public void displayScore(){
        // see how this fits in with the current game screen 
        Console.WriteLine("Player 1 score: " + player1Score + " | Player 2 score: " + player2Score);
    }

    public void updateLeaderboard(string winningPlayer, string loosingPlayer){
        // update db with one win, one loss
    }

}
}