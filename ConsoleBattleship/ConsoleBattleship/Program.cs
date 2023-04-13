// See https://aka.ms/new-console-template for more information
using ConsoleBattleship.states;

Console.WriteLine("Hello, World!");
// The templates have changed, in main, we no longer see the top level statements normally used, this can be a pain, but its part
// of .NET 6 and I thought it best to go with the latest LTS support

//State machine constants
const string EXAMPLE = "Example";
const string SECOND = "Second";
//See implementation and expansion of the state machine
//Append new states by adding them to the dictionary
Dictionary<string, BaseState> states = new Dictionary<string, BaseState>()
{
    {EXAMPLE, new StateExample()},
    {SECOND, new SecondStateExample() }
};
StateMachine sm = new StateMachine(states);

sm.changeState(EXAMPLE, new object[]{sm, "123"});

//temporary infinite loop
while (true)
{
    sm.updateState();
}