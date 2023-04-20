using ConsoleBattleship;

Screen screen = Screen.GetScreen(40, 20);

//screen.Start();
//while (true) for (int i = 0; i < 100; i++)
//{
//  BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
//  BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 15, 2 * i, 90 );
//  Thread.Sleep(500);
//  BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
//  BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 15 , 2 * i, 90);
//}
//screen.stop();

using ConsoleBattleship.states;
using ConsoleBattleship;
using System.Data.SQLite;

DbHandler dbHandler = new DbHandler();

string url = @"URI=file:battleship.db";
using var connection = new SQLiteConnection(url);
connection.Open();


StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.EXAMPLE, new object[] { "123" });

//temporary infinite loop
while (true)
{
    StateMachine.StateMachineInstance.UpdateState();
}
