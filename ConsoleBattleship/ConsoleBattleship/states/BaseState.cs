using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    /// <summary>
    /// This class serves as the parent/base for each state in the state machine, within each state, these functions can be called/overridden 
    /// even if they do not exist
    /// </summary>
    internal class BaseState
    {
        //constructor
        public virtual void Init(params object[] args) { }
        //what to do on entry
        public virtual void Enter(params object[] args) { }
        //what to do on exit
        public virtual void Exit(params object[] args) { }
        //Looping function that will handle all logic done every event/repetition
        public virtual void Update(params object[] args) { }
        //what is rendered to the screen
        public virtual void Render(params object[] args) { }
    }
}
