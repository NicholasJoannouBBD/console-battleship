using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    internal class Carrier : Ship
    {
        public Carrier()
        {
            ShipName = "Aircraft Carrier";
            MaximumHits = 5;
        }
    }
}
