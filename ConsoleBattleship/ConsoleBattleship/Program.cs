// See https://aka.ms/new-console-template for more information
using ConsoleBattleship;

Screen screen = Screen.GetScreen(40, 20);

screen.Start();
while (true) for (int i = 0; i < 100; i++)
{
  BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
  BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 15, 2 * i, 90 );
  Thread.Sleep(500);
  BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
  BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 15 , 2 * i, 90);



}



//screen.stop();
// The templates have changed, in main, we no longer see the top level statements normally used, this can be a pain, but its part
// of .NET 6 and I thought it best to go with the latest LTS support