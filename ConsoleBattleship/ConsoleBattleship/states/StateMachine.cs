using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    internal class StateMachine
    {
        static StateMachine stateMachineInstance;
        static readonly object lockSM = new object();
        static BaseState empty = new BaseState();
        static Dictionary<string, BaseState>? states;
        static BaseState current;
        public static StateMachine StateMachineInstance
        {
            get
            {
                lock (lockSM)
                {
                    if(stateMachineInstance == null)
                    {
                        stateMachineInstance = new StateMachine();
                    }
                    return stateMachineInstance;
                }
            }
        }

        //State machine constants
        const string _EXAMPLE = "Example";
        const string _SECOND = "Second";

        public string EXAMPLE { get { return _EXAMPLE; } }
        public string SECOND { get { return _SECOND; } }
        private StateMachine() 
        {
            //See implementation and expansion of the state machine
            //Append new states by adding them to the dictionary
            Dictionary<string, BaseState> statesRef = new Dictionary<string, BaseState>()
            {
                {EXAMPLE, new StateExample()},
                {SECOND, new SecondStateExample() }
            };
            states = statesRef;
            current = empty;
        }

        //can be changed to bool return to validate state change
        public void changeState(string stateName, params object[] args)
        {
            try
            {
                if (states.ContainsKey(stateName))
                {
                    current.exit();
                    current = states[stateName];
                    current.enter(args);
                }
                else
                {
                    //state does not exist
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("This state does not exist");
            }
        }

        public void updateState()
        {
            current.update();
        }

        public void renderState()
        {
            current.render();
        }
    }
}
