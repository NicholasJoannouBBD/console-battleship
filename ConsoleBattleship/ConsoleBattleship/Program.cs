using ConsoleBattleship;
using ConsoleBattleship.states;
using System.Data.SQLite;

DbHandler dbHandler = DbHandler.Instance;

string url = @"URI=file:battleship.db";
using var connection = new SQLiteConnection(url);
connection.Open();


StateMachine.StateMachineInstance.ChangeState(StateMachine.MENU, new object[] {});

//temporary infinite loop
while (true)
{
    StateMachine.StateMachineInstance.UpdateState();
}
