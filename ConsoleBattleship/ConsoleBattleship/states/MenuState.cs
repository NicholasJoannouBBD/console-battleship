using ConsoleBattleship.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
  internal class MenuState : BaseState
  {
    private MenuScreen screen = MenuScreen.GetScreen();

    public override void Enter(params object[] args)
    {
      screen.OnSelectedMenuItem += (string item) =>
      {
        if (item == "Host")
        {
          screen.Stop();
          StateMachine.StateMachineInstance.ChangeState(StateMachine.FINDGAME);
        }
      };
      screen.Start();
    }

    public override void Exit(params object[] args)
    {
      screen.Stop();
    }

    public override void Render(params object[] args)
    {
      //this is all the output to go on the screen.
      screen.Refresh();
    }

    public override void Update(params object[] args)
    {
        //function is used to update some logic on an event call, or tick rate.
        this.Render();
        //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
    }
  }
}
