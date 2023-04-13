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
        StateMachine sm;
        public override void enter(params object[] args)
        {
            this.sm = (StateMachine)args[0];
            this.exampleParam = (string)args[1];
        }

        public override void exit(params object[] args)
        {
            Console.WriteLine("Exiting Example state");
        }

        public override void render(params object[] args)
        {
            //this is all the output to go on the screen.
        }

        public override void update(params object[] args)
        {
            this.render();
            //function is used to update some logic on an event call, or tick rate.
            Console.WriteLine("Updating... " + this.exampleParam);
            Console.WriteLine("Should I exit? Y/N");
            if (Console.ReadLine().Equals("Y"))
            {
                sm.changeState("Second", new object[] { sm,"Now State 2" });
            }
        }
    }
}
