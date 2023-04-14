// See https://aka.ms/new-console-template for more information
using ConsoleBattleship.states;

Console.WriteLine("Hello, World!");
// The templates have changed, in main, we no longer see the top level statements normally used, this can be a pain, but its part
// of .NET 6 and I thought it best to go with the latest LTS support


StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.EXAMPLE, new object[]{"123"});

//temporary infinite loop
while (true)
{
    StateMachine.StateMachineInstance.UpdateState();
}