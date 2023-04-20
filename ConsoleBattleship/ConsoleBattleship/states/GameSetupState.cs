using ConsoleBattleship.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
internal class GameSetupState : BaseState
{
  GameScreen screen = GameScreen.GetScreen(40, 20);
  public override void Enter(params object[] args)
  {
    screen.Start();
  }

  public override void Exit(params object[] args)
  {
      screen.Stop();
  }

    public override void Render(params object[] args)
  {
      //this is all the output to go on the screen.
  }

  public override void Update(params object[] args)
  {
      //function is used to update some logic on an event call, or tick rate.
      this.Render();
      for (int i = 0; i < 100; i++)
      {
        BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
        BattleshipDrawer.Draw3x1Battleship(screen.BattleshipGrid, 15, 2 * i, 90);
        Thread.Sleep(500);
        BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 10, 10, 30 * i);
        BattleshipDrawer.UndoDraw3x1Battleship(screen.BattleshipGrid, 15, 2 * i, 90);
      }
      //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
    }
}
}
