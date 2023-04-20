using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string response;
                Console.Write("\nUsername : ");
                string requestMessage = Console.ReadLine();
                response = ClientSend.Username(requestMessage);
                Console.WriteLine(response);

                Console.Write("X coords : ");
                string x = Console.ReadLine();
                Console.Write("Y coords : ");
                string y = Console.ReadLine();

                response = ClientSend.SendX(x);
                Console.WriteLine(response);
                response = ClientSend.SendY(y);
                Console.WriteLine(response);
            }
        }
    }
}