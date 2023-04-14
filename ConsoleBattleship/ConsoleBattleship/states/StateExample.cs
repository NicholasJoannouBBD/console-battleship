using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    /// <summary>
    /// This class serves as an example on how to implement a state, this state will be removed before finalising the project
    /// </summary>
    internal class StateExample : BaseState
    {
        string exampleParam;
        
        public override void enter(params object[] args)
        {
            this.exampleParam = (string)args[0];
        }

        public override void exit(params object[] args)
        {
            Console.WriteLine("Exiting Example state");
        }

        public override void render(params object[] args)
        {
            //this is all the output to go on the screen.
            Console.WriteLine("Updating... " + this.exampleParam);
            Console.WriteLine("Should I exit? Y/N");
            if (Console.ReadLine().Equals("Y"))
            {
                StateMachine.StateMachineInstance.changeState(StateMachine.StateMachineInstance.SECOND, new object[] { "Now State 2" });
            }
        }

        public override void update(params object[] args)
        {
            this.render();
            //function is used to update some logic on an event call, or tick rate.
        }
    }
}
