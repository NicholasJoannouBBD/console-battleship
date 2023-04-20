using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    internal class GameSetupState : BaseState
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
            //this is all the output to go on the screen.
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
            //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
        }
    }
}
