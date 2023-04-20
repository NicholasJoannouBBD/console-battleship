using Pastel;

namespace ConsoleBattleship.Screen
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
          grid.ReplaceChar(row - 1, column, "▲".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column,     "█".Pastel(Colors.Battleship));
          grid.ReplaceChar(row + 1, column, "▀".Pastel(Colors.Battleship));
          break;
        case 1:
          // EAST
          grid.ReplaceChar(row, column + 1, "►".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column,     "█".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column - 1, "▐".Pastel(Colors.Battleship));
          break;
        case 2:
          // SOUTH
          grid.ReplaceChar(row - 1, column, "▄".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column,     "█".Pastel(Colors.Battleship));
          grid.ReplaceChar(row + 1, column, "▼".Pastel(Colors.Battleship));
          break;
        case 3:
          // WEST
          grid.ReplaceChar(row, column + 1, "▌".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column,     "█".Pastel(Colors.Battleship));
          grid.ReplaceChar(row, column - 1, "◄".Pastel(Colors.Battleship));
          break;

      }
    }

    public static void UndoDraw3x1Battleship(Grid grid, int row, int column, int orientation)
    {
      switch (orientation % 360 / 90)
      {
        case 0:
          // NORTH
          grid.ReplaceChar(row + 1, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row - 1, column, GameScreen.BackgroundChar);
          break;
        case 1:
          // EAST
          grid.ReplaceChar(row, column + 1, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column - 1, GameScreen.BackgroundChar);
          break;
        case 2:
          // SOUTH
          grid.ReplaceChar(row + 1, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row - 1, column, GameScreen.BackgroundChar);
          break;
        case 3:
          // WEST
          grid.ReplaceChar(row, column + 1, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column, GameScreen.BackgroundChar);
          grid.ReplaceChar(row, column - 1, GameScreen.BackgroundChar);
          break;

      }
    }

  }
}
