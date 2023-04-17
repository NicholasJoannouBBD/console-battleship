using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    internal class Destroyer : Ship
    {
        public Destroyer()
        {
            ShipName = "Destroyer";
            MaximumHits = 2;
        }
    }
}
