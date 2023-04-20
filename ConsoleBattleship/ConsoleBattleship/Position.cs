using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
    internal class Position
    {
        public int Row;
        public int Column;

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
