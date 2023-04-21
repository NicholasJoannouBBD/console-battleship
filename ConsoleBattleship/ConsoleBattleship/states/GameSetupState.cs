using ConsoleBattleship.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship.Sockets;

namespace ConsoleBattleship.states
{
    internal class GameSetupState : BaseState
    {
        GameScreen screen = GameScreen.GetScreen(40, 20);
        Client c1;
        Game game;
        public override void Enter(params object[] args)
        {
            this.c1 = (Client)args[0];
            c1.Setup();
            this.game = new Game(screen.BattleshipGrid.Width, screen.BattleshipGrid.Height);
            this.game.Player1.SetupShips();
            screen.Start();
        }

        public override void Exit(params object[] args)
        {
            screen.Stop();
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
            game.Player1.OutputBoards(game.Player1.GameBoard, game.Player1.FiringBoard, screen.BattleshipGrid);
            //Console.WriteLine(c1);
            c1.Setup();
            //Console.ReadLine();
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
            string res = c1.ListenerClient();
            if (res.Equals("PLAY"))
            {
                //StateMachine.StateMachineInstance.ChangeState(StateMachine.TURN, new object[] { c1 });
            }
            //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
        }
    }
}
