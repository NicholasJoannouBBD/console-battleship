using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    internal class SecondStateExample : BaseState
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
            throw new NotImplementedException();
        }

        public override void render(params object[] args)
        {
            //this is all the output to go on the screen.
        }

        public override void update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.render();
            Console.WriteLine("Updating... " + this.exampleParam);
        }
    }
}
