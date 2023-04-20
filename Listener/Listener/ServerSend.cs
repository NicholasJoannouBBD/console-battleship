using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    public class ServerSend
    {
        public static void sendMessage(string message, TcpClient client)
        {
            byte[] bytes = Helper.messageToByteArray(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public static string UsernameReceived(string message)
        {
            string result = "";
            foreach (var c in message)
                if (c != '\0')
                    result += c;

            Console.WriteLine($"Connection from {result}");
            return "Player connected";
        }

        private static string x = "";
        private static string y = "";

        public static string xCoordsReceived(string message)
        {
            string result = "";
            foreach (var c in message)
                if (c != '\0')
                    result += c;

            Console.WriteLine($"X coord: {result}");
            x = result;
            return "Waiting for y coord";
        }

        public static string yCoordsReceived(string message)
        {
            string result = "";
            foreach (var c in message)
                if (c != '\0')
                    result += c;

            Console.WriteLine($"Y coord: {result}");
            y = result;
            Console.WriteLine($"Player shot {x}, {y}");
            return $"Player shot {x}, {y}";
        }
    }
}
