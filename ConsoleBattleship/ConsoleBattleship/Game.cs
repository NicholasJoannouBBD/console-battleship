using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
    internal class Game
    {
        public User Player1 { get; set;}
        public User Player2 { get; set; }

        public Game(int width, int height) {
            //just an example for now but users will be associated with the database
            Player1 = new User(1, "Motheo", 0, 0, width, height);
            Player2 = new User(2, "Tsepo", 3, 4, width, height);

            Player1.SetupShips();
            Player2.SetupShips();
            /*
            Player1.OutputBoards(Player1.GameBoard);
            Player2.OutputBoards(Player2.GameBoard);
            */
        }
    }
}
