// See https://aka.ms/new-console-template for more information
using ConsoleBattleship;

Screen screen = Screen.GetScreen(80, 20);

screen.Start();

//screen.stop();
// The templates have changed, in main, we no longer see the top level statements normally used, this can be a pain, but its part
// of .NET 6 and I thought it best to go with the latest LTS support