using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
    internal class GameBoard
    {
        public List<Cell> Cells { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;

            Cells = new List<Cell>();
            for(int i = 1; i <= width; i++)
            {
                for(int j = 1; j <= height; j++)
                {
                    Cells.Add(new Cell(i, j));
                }
            }
        }
    }
}
