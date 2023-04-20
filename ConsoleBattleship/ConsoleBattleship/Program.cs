using ConsoleBattleship;
using ConsoleBattleship.Screen;
using ConsoleBattleship.states;
using System.Data.SQLite;

MenuScreen menuScreen = MenuScreen.GetScreen(40, 20);

menuScreen.OnSelectedMenuItem += (string item) =>
{
  if (item == "Start")
  {
    menuScreen.Stop();  
    GameScreen gameScreen = GameScreen.GetScreen(40, 20);
    gameScreen.Start();
    //while (true) 
    for (int i = 0; i < 100; i++)
    {
      BattleshipDrawer.Draw3x1Battleship(gameScreen.BattleshipGrid, 10, 10, 30 * i);
      BattleshipDrawer.Draw3x1Battleship(gameScreen.BattleshipGrid, 15, 2 * i, 90);
      Thread.Sleep(500);
      BattleshipDrawer.UndoDraw3x1Battleship(gameScreen.BattleshipGrid, 10, 10, 30 * i);
      BattleshipDrawer.UndoDraw3x1Battleship(gameScreen.BattleshipGrid, 15, 2 * i, 90);
    }

    gameScreen.Stop();
  }
};

menuScreen.Start();


DbHandler dbHandler = new DbHandler();

string url = @"URI=file:battleship.db";
using var connection = new SQLiteConnection(url);
connection.Open();


StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.EXAMPLE, new object[] { "123" });

//temporary infinite loop
while (true)
{
    //StateMachine.StateMachineInstance.UpdateState();
}
