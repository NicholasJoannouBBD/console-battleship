using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship.Ships;

namespace ConsoleBattleship
{
    internal class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public Grid GameBoard { get; set; }
        public List<Ship> Ships { get; set; } 

        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public User(int userId, string userName, int wins, int loses)
        {
            UserId = userId;
            UserName = userName;
            Wins = wins;
            Loses = loses;
            GameBoard = new Grid(10, 10, "initial");
            Ships = new List<Ship>()
            {
                new BattleShip(),
                new Destroyer(),
                new Carrier(),
                new Cruiser(),
                new Submarine()
            };
        }

        public void SetupShips()
        {
            //place each ship on the player's board
        }
    }
}
