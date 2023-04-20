using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    public class Helper
    {
        static Encoding encoding = Encoding.UTF8;
        public static byte[] messageToByteArray(string message)
        {
            byte[] messageBytes = encoding.GetBytes(message);
            int messageSize = messageBytes.Length;
            int completeSize = messageSize + 4;
            byte[] completemsg = new byte[completeSize];

            byte[] sizeBytes = BitConverter.GetBytes(messageSize);
            sizeBytes.CopyTo(completemsg, 0);
            messageBytes.CopyTo(completemsg, 4);
            return completemsg;
        }

        public static string streamToMessage(Stream stream)
        {
            byte[] sizeBytes = new byte[4];
            byte[] typeBytes = new byte[4];
            stream.Read(sizeBytes, 0, 4);
            int messageSize = BitConverter.ToInt32(sizeBytes, 0);
            stream.Read(typeBytes, 0, 4);
            int packageType = BitConverter.ToInt32(typeBytes, 0);
            byte[] messageBytes = new byte[messageSize];
            stream.Read(messageBytes, 0, messageSize);
            string message = encoding.GetString(messageBytes);

            switch (packageType)
            {
                case (int)ClientPackets.username:
                    return ServerSend.UsernameReceived(message);
                default:
                    return "Unknown packet";
            }
        }

        
    }
}
