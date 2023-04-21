using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship;


namespace ConsoleBattleship.states
{
    internal class HighScoreState : BaseState
    {
        public override void Enter(params object[] args)
        {
            //"Constructor" of the state
        }

        public override void Exit(params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void Render(params object[] args)
        {
            Score score = new Score();
            score.displayLeaderboard();
        }

        public override void Update(params object[] args)
        {
            this.Render();
        }
    }
}
