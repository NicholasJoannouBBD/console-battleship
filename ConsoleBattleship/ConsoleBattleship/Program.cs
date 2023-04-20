using ConsoleBattleship;
using ConsoleBattleship.states;
using System.Data.SQLite;

StateMachine.StateMachineInstance.ChangeState(StateMachine.LOGIN, new object[] { });

//temporary infinite loop
while (true)
{
    StateMachine.StateMachineInstance.UpdateState();
}