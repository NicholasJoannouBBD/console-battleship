using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
    public enum CellOccupation
    {
        Empty,
        BattleShip,
        Carrier,
        Cruiser,
        Destroyer,
        Submarine,
        Hit,
        Miss
    }
}
