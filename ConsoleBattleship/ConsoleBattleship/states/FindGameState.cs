using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship.Sockets;

namespace ConsoleBattleship.states
{
    internal class FindGameState : BaseState
    {
        Client c1 = new Client();
        public override void Enter(params object[] args)
        {
            //"Constructor" of the state
            c1.ConnectClient();
        }

        public override void Exit(params object[] args)
        {
            c1.Exit();
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
            string resp = c1.ListenerClient();
            if (resp.Equals("Ready"))
            {
                StateMachine.StateMachineInstance.ChangeState(StateMachine.GAMESETUP, new object[] { c1 });
            }
            //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
        }
    }
}
