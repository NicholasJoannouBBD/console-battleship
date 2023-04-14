using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.states
{
    /// <summary>
    /// Singleton class that enables states to access the collection of states to switch between one another without creating
    /// references to a new StateMachine object.
    /// </summary>
    internal class StateMachine
    {
        #region Declarations
        //Singleton instance
        static StateMachine stateMachineInstance;
        static readonly object lockSM = new object();
        //empty state
        static BaseState empty = new BaseState();
        //collection of existing states
        static Dictionary<string, BaseState>? states;
        //current state
        static BaseState current;
        #endregion

        #region Constants
        //State machine constants for keys
        const string _EXAMPLE = "Example";
        const string _SECOND = "Second";
        #endregion

        #region Properties
        public string EXAMPLE { get { return _EXAMPLE; } }
        public string SECOND { get { return _SECOND; } }
        public static StateMachine StateMachineInstance
        {
            get
            {
                lock (lockSM)
                {
                    if (stateMachineInstance == null)
                    {
                        stateMachineInstance = new StateMachine();
                    }
                    return stateMachineInstance;
                }
            }
        }
        #endregion

        #region Constructor
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
        #endregion

        #region public Methods
        public void ChangeState(string stateName, params object[] args)
        {
            bool exists = states?.ContainsKey(stateName) ?? false;
            if (exists)
            {
                current.Exit();
                current = states?[stateName]??empty;
                current.Enter(args);
            }
            else
            {
                //state does not exist
                //decide what to do
            }
        }

        public void UpdateState()
        {
            current.Update();
        }

        public void RenderState()
        {
            current.Render();
        }
        #endregion
    }
}
