using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    /// <summary>
    /// This class serves as the parent/base for each state in the state machine, within each state, these functions can be called 
    /// even if they do not exist
    /// </summary>
    internal class BaseState
    {
        //constructor
        public virtual void init(params object[] args) { }
        //what to do on entry
        public virtual void enter(params object[] args) { }
        //what to do on exit
        public virtual void exit(params object[] args) { }
        //Looping function that will handle all logic done every event/repetition
        public virtual void update(params object[] args) { }
        //what is rendered to the screen
        public virtual void render(params object[] args) { }

        
    }
}
