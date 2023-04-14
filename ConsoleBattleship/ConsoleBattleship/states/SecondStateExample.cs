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
        public override void Enter(params object[] args)
        {
            this.exampleParam = (string)args[0];
        }

        public override void Exit(params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
            Console.WriteLine("Updating... " + this.exampleParam);
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
        }
    }
}
