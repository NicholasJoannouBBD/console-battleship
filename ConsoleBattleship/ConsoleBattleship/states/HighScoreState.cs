using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ConsoleBattleship.Score;
using ConsoleBattleship.Screen;

namespace ConsoleBattleship.states
{
    internal class HighScoreState : BaseState
    {
        private HighScoreScreen screen = HighScoreScreen.GetScreen();
        public override void Enter(params object[] args)
        {
            screen.OnSelectedItem += (string item) =>
            {
                /*if (item == "Host")
                {
                    screen.Stop();
                    StateMachine.StateMachineInstance.ChangeState(StateMachine.GAMESETUP);
                }
                if (item == "Quit")
                {
                    screen.Stop();
                    StateMachine.StateMachineInstance.ChangeState(StateMachine.EXIT);
                }*/
            };
            screen.Start();
        }

        public override void Exit(params object[] args)
        {
            screen.Stop();
            //throw new NotImplementedException();
        }

        public override void Render(params object[] args)
        {
            // check if correct
            //Score thingy = new Score();
           // screen.Refresh();


            /*Score thingy = new Score();
            thingy.displayLeaderboard();*/
        }

        public override void Update(params object[] args)
        {
            this.Render();
        }
    }
}
