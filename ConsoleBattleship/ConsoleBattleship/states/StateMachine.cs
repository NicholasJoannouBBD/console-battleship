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
        const string _LOGIN = "Login";
        const string _CREATEACCOUNT = "CreateAccount";
        const string _MENU = "Menu";
        const string _PROFILE = "Profile";
        const string _FINDGAME = "FindGame";
        const string _PLAYERDETAILS = "Details";
        const string _HIGHSCORES = "HighScores";
        const string _GAMESETUP = "Setup";
        const string _TURN = "Turn";
        const string _WAIT = "Wait";
        const string _GAMEOVER = "GameOver";
        const string _EXIT = "Exit";
        #endregion

        #region Properties
        public static string EXAMPLE => _EXAMPLE;
        public static string SECOND => _SECOND;
        public static string LOGIN => _LOGIN;

        public static string CREATEACCOUNT => _CREATEACCOUNT;

        public static string MENU => _MENU;

        public static string PROFILE => _PROFILE;

        public static string FINDGAME => _FINDGAME;

        public static string PLAYERDETAILS => _PLAYERDETAILS;

        public static string HIGHSCORES => _HIGHSCORES;

        public static string GAMESETUP => _GAMESETUP;

        public static string TURN => _TURN;

        public static string WAIT => _WAIT;

        public static string GAMEOVER => _GAMEOVER;

        public static string EXIT => _EXIT;
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
                {LOGIN, new LoginState() },
                {CREATEACCOUNT, new CreateAccountState() },
                {MENU, new MenuState() },
                {PROFILE, new ProfileState() },
                {FINDGAME, new FindGameState() },
                {PLAYERDETAILS, new PlayerDetailsState() },
                {HIGHSCORES, new HighScoreState() },
                {GAMESETUP, new GameSetupState() },
                {TURN, new TurnState() },
                {WAIT, new WaitState() },
                {GAMEOVER, new GameOverState() },
                {EXIT, new ExitState() }
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
