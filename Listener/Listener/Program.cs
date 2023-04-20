using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System;
using System.Text;

namespace Listener
{
    public class Program
    {
        public static bool player1Connected = false;
        public static bool player2Connected = false;

        public static void Main(string[] args)
        {
            Console.WriteLine("Server starting!");
            IPAddress ipAddr = IPAddress.Loopback;
            int port = 8888;
            IPEndPoint ep = new IPEndPoint(ipAddr, port);
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);

            while (true)
            {
                var sender = listener.AcceptTcpClient();
                string responseMessage = Helper.streamToMessage(sender.GetStream());
                if (responseMessage != null)
                {
                    ServerSend.sendMessage(responseMessage, sender);
                }
            }
        }

        
    }
}