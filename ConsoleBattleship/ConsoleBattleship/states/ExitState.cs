namespace ConsoleBattleship.states
{
    internal class ExitState : BaseState
    {
        public override void Enter(params object[] args)
        {
            //"Constructor" of the state
        }

        public override void Exit(params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void Render(params object[] args)
        {
            //this is all the output to go on the screen.
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(goodbye());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Turning off mining hardware...");
            Thread.Sleep(200);
            Console.WriteLine("Successfully mined 3BTC");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to close mining tools...");
            Console.WriteLine("System resource overload:");
            Console.WriteLine("CPU @101%");
            Console.WriteLine("MEMOERY @1.3TB");
            Console.WriteLine("HDD @10kb/s");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting Mining hardware...");
            Thread.Sleep(300);
            Console.WriteLine("Mining hardware activated!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Plucking chickens...");
            Thread.Sleep(150);
            Console.WriteLine("Adjusting AWAZURE Load balancers...");
            Thread.Sleep(150);
            Console.WriteLine("Closing...");
            Environment.Exit(0);
        }

        public override void Update(params object[] args)
        {
            //function is used to update some logic on an event call, or tick rate.
            this.Render();
            //StateMachine.StateMachineInstance.ChangeState(StateMachine.StateMachineInstance.<STATE_NAME>, new object[] { <PARAMS> });
        }

        private string goodbye()
        {
            string l1 = @"
 _____                 _ _                
|  __ \               | | |               
| |  \/ ___   ___   __| | |__  _   _  ___ 
| | __ / _ \ / _ \ / _` | '_ \| | | |/ _ \
| |_\ \ (_) | (_) | (_| | |_) | |_| |  __/
 \____/\___/ \___/ \__,_|_.__/ \__, |\___|
                                __/ |     
                               |___/      
                        ";
            return l1;
        }
    }
}
