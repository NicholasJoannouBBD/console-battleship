using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    //Base class to represent the 5 types of ships in the game
    public abstract class Ship
    {
        public string ShipName { get; set; }
        //count of the hits a ship takes
        public int Hits { get; set; }
        //maximum hits that a ship can take
        public int MaximumHits { get; set; }

        //check if the ship is sunken
        public bool IsSunk
        {
            //return true if the number of hit is greater than or equal to the maximum hits
            get { return Hits >= MaximumHits; }
        }
    }
}
