using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    internal class StateMachine
    {
        BaseState empty = new BaseState();
        Dictionary<string, BaseState>? states;
        BaseState current;
        public Dictionary<string, BaseState> States { get; set; }

        public StateMachine(Dictionary<string, BaseState> states)
        {
            this.States = states??new Dictionary<string, BaseState>() { };
            this.current = empty;
        }

        //can be changed to bool return to validate state change
        public void changeState(string stateName, params object[] args)
        {
            if (States.ContainsKey(stateName))
            {
                this.current.exit();
                this.current = this.States[stateName];
                this.current.enter(args);
            }
            else
            {
                //state does not exist
            }
        }

        public void updateState()
        {
            this.current.update();
        }

        public void renderState()
        {
            this.current.render();
        }
    }
}
