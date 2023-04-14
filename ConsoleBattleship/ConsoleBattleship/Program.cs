// See https://aka.ms/new-console-template for more information
using ConsoleBattleship;

Screen screen = Screen.GetScreen(80, 20);

screen.Start();

for (int i = 0; i < 100; i++)
{
  BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 10, 60, 90 * i);
  Thread.Sleep(100);
  BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 10, 60, 90 * i);
}

screen.BattleshipGrid.ReplaceChar(10, 40, "O");


//screen.stop();
// The templates have changed, in main, we no longer see the top level statements normally used, this can be a pain, but its part
// of .NET 6 and I thought it best to go with the latest LTS support