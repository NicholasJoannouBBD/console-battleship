using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    public class ServerSend
    {
        public static string UsernameReceived(string message)
        {
            string result = "";
            foreach (var c in message)
                if (c != '\0')
                    result += c;

            Console.WriteLine($"Connection from {result}");
            return "Player connected";
        }
    }
}
