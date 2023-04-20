using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
    internal class Cell
    {
        public CellOccupation CellOccupation { get; set; }
        public Position Position { get; set; }

        public Cell(int row, int column)
        {
            Position = new Position(row, column);
            CellOccupation = CellOccupation.Empty;
        }

        public Cell getCell()
        {
            return this;
        }

        public string Status
        {
            get
            {
                if (CellOccupation == CellOccupation.Empty)
                    return "o";
                else if (CellOccupation == CellOccupation.BattleShip)
                    return "B";
                else if (CellOccupation == CellOccupation.Carrier)
                    return "A";
                else if (CellOccupation == CellOccupation.Cruiser)
                    return "C";
                else if (CellOccupation == CellOccupation.Destroyer)
                    return "D";
                else if (CellOccupation == CellOccupation.Submarine)
                    return "S";
                else if (CellOccupation == CellOccupation.Miss)
                    return "M";
                else if (CellOccupation == CellOccupation.Hit)
                    return "X";
                else
                    return "";
            }
        }

        public bool IsOccupied
        {
            get{
                return CellOccupation == CellOccupation.BattleShip
                || CellOccupation == CellOccupation.Carrier
                || CellOccupation == CellOccupation.Cruiser
                || CellOccupation == CellOccupation.Submarine
                || CellOccupation == CellOccupation.Destroyer;
            }
        }
    }
}
