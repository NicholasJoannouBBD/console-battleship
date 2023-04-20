using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Helper
    {
        static Encoding encoding = Encoding.UTF8;
        public static byte[] messageToByteArray(int packetType, string message)
        {
            byte[] messageBytes = encoding.GetBytes(message);
            int messageSize = messageBytes.Length;
            int completeSize = messageSize + 8;
            byte[] completemsg = new byte[completeSize];

            byte[] sizeBytes = BitConverter.GetBytes(messageSize);
            byte[] typeBytes = BitConverter.GetBytes(packetType);
            sizeBytes.CopyTo(completemsg, 0);
            typeBytes.CopyTo(completemsg, 4);
            messageBytes.CopyTo(completemsg, 8);
            return completemsg;
        }

        public static string streamToMessage(Stream stream)
        {
            byte[] sizeBytes = new byte[4];
            stream.Read(sizeBytes, 0, 4);
            int messageSize = BitConverter.ToInt32(sizeBytes, 0);
            byte[] messageBytes = new byte[messageSize];
            stream.Read(messageBytes, 0, messageSize);
            string message = encoding.GetString(messageBytes);
            string result = null;
            foreach (var c in message)
                if (c != '\0')
                    result += c;

            return result;
        }
    }
}
