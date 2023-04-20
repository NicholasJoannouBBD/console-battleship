using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Program
    {
        public static int playerID;
        static void Main(string[] args)
        {
            playerID = ClientSend.Connect();
            Console.WriteLine(playerID);

            string resp = ClientSend.Disconnect();
            Console.WriteLine(resp);


            while (true)
            {
                string response;
                Console.Write("\nUsername : ");
                string requestMessage = Console.ReadLine();
                response = ClientSend.Username(requestMessage);
                Console.WriteLine(response);

                Console.Write("X coords : ");
                string x = Console.ReadLine();
                response = ClientSend.SendX(x);
                Console.WriteLine(response);

                Console.Write("Y coords : ");
                string y = Console.ReadLine();
                response = ClientSend.SendY(y);
                Console.WriteLine(response);
            }
        }
    }
}