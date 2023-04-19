using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Score
{
    public class Score {
    private int player1Score;
    private int player2Score;
    
    public Score()
    {
        player1Score = 0;
        player2Score = 0;
    }
    
    public void IncrementPlayer1Score(int points)
    {
        player1Score =+ points;
    }
    
    public void IncrementPlayer2Score(int points)
    {
        player2Score =+ points;
    }
    
    public int GetPlayer1Score()
    {
        return player1Score;
    }
    
    public int GetPlayer2Score()
    {
        return player2Score;
    }
    
    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
    }
}
}