﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    internal class Cruiser : Ship
    {
        public Cruiser()
        {
            ShipName = "Cruiser";
            Size = 3;
            CellOccupation = CellOccupation.Cruiser;
        }
    }
}
