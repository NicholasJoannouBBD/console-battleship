using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship
{
  partial class BattleshipDrawer
  {

    /// <param name="orientation">In degrees, converted to NESW</param>
    public static void Draw3x1Battleship(Grid grid, int row, int column, int orientation)
    {
      switch (orientation % 360 / 90)
      {
        case 0:
          // NORTH
          grid.ReplaceChar(row - 1, column, "┃");
          grid.ReplaceChar(row, column,     "╬");
          grid.ReplaceChar(row + 1, column, "╹");
          break;
        case 1:
          // EAST
          grid.ReplaceChar(row, column + 1, "─");
          grid.ReplaceChar(row, column,     "╬");
          grid.ReplaceChar(row, column - 1, "╺");
          break;
        case 2:
          // SOUTH
          grid.ReplaceChar(row - 1, column, "╻");
          grid.ReplaceChar(row, column,     "╬");
          grid.ReplaceChar(row + 1, column, "┃");
          break;
        case 3:
          // WEST
          grid.ReplaceChar(row, column + 1, "╸");
          grid.ReplaceChar(row, column,     "╬");
          grid.ReplaceChar(row, column - 1, "─");
          break;

      }
    }

    public static void UndoDraw3x1Battleship(Grid grid, int row, int column, int orientation)
    {
      switch (orientation % 360 / 90)
      {
        case 0:
          // NORTH
          grid.ReplaceChar(row + 1, column, Screen.s_background);
          grid.ReplaceChar(row, column, Screen.s_background);
          grid.ReplaceChar(row - 1, column, Screen.s_background);
          break;
        case 1:
          // EAST
          grid.ReplaceChar(row, column + 1, Screen.s_background);
          grid.ReplaceChar(row, column, Screen.s_background);
          grid.ReplaceChar(row, column - 1, Screen.s_background);
          break;
        case 2:
          // SOUTH
          grid.ReplaceChar(row + 1, column, Screen.s_background);
          grid.ReplaceChar(row, column, Screen.s_background);
          grid.ReplaceChar(row - 1, column, Screen.s_background);
          break;
        case 3:
          // WEST
          grid.ReplaceChar(row, column + 1, Screen.s_background);
          grid.ReplaceChar(row, column, Screen.s_background);
          grid.ReplaceChar(row, column - 1, Screen.s_background);
          break;

      }
    }

  }
}
