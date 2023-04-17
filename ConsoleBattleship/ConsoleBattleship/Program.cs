// See https://aka.ms/new-console-template for more information
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