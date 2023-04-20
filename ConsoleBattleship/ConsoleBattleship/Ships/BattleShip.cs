using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.Ships
{
    internal class BattleShip : Ship
    {
        public BattleShip()
        {
            ShipName = "BattleShip";
            Size = 4;
            CellOccupation = CellOccupation.BattleShip;
        }
    }
}
