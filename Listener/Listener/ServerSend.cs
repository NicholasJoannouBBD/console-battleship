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

        public static string ClientConnected(string message)
        {
            if (!Program.player1Connected)
            {
                Program.player1Connected = true;
                Console.WriteLine("Player 1 connected");
                return "1";
            }
            else if (!Program.player2Connected)
            {
                Program.player2Connected = true;
                Console.WriteLine("Player 2 connected");
                return "2";
            }
            else
            {
                return "-1";
            }
        }

        public static string ClientDisconnected(string playerID)
        {
            if (playerID == "1")
            {
                Program.player1Connected = false;
                Console.WriteLine("Player 1 disconnected");
                return "Disconnected";
            }
            else if (!Program.player2Connected)
            {
                Program.player2Connected = false;
                Console.WriteLine("Player 2 disconnected");
                return "Disconnected";
            }

            return "Player was never connected";
        }
    }
}
