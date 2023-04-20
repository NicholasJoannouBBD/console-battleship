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
        Game game = new Game();

        public override void Enter(params object[] args)
        {
            this.exampleParam = (string)args[0];
        }

        public override void Exit(params object[] args)
        {
            Console.WriteLine("Exiting Example state");
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
            game.Player1.OutputBoards(game.Player1.GameBoard, game.Player1.FiringBoard);
            game.Player2.OutputBoards(game.Player2.GameBoard, game.Player2.FiringBoard);
            if (Console.ReadLine().Equals("Y"))
            {
                StateMachine.StateMachineInstance.ChangeState(StateMachine.SECOND, new object[] { "Now State 2" });
            }
        }

        public override void Update(params object[] args)
        {
            this.Render();
            //function is used to update some logic on an event call, or tick rate.
        }
    }
}
