using ConsoleBattleship;
using ConsoleBattleship.states;
using System.Data.SQLite;

StateMachine.StateMachineInstance.ChangeState(StateMachine.FINDGAME, new object[] { });

//temporary infinite loop
while (true)
{
    StateMachine.StateMachineInstance.UpdateState();
}