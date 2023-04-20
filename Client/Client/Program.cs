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
                Console.Write("\nProcess message : ");
                string requestMessage = Console.ReadLine();
                string response = ClientSend.Username(requestMessage);
                Console.WriteLine(response);
            }
        }
    }
}