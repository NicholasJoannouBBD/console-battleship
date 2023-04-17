using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    internal class Submarine : Ship
    {
        public Submarine()
        {
            ShipName = "Submarine";
            MaximumHits = 3;
        }
    }
}
