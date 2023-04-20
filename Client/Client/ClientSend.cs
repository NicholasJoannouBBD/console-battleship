﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientSend
    {
        public static string sendMessage(int packetType, string message)
        {
            string response = "";
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                client.NoDelay = true;
                byte[] messageBytes = Helper.messageToByteArray(packetType, message);

                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(messageBytes, 0, messageBytes.Length);
                    response = Helper.streamToMessage(stream);
                }
                client.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return response;
        }

        // Username packet
        public static string Username(string _username)
        {
            return sendMessage((int)ClientPackets.username, _username);
        }

        // Send coords packets
        public static string SendX(string _x)
        {
            return sendMessage((int)ClientPackets.xCoords, _x);
        }

        public static string SendY(string _y)
        {
            return sendMessage((int)ClientPackets.yCoords, _y);
        }

        // Connect
        public static int Connect()
        {
            return int.Parse(sendMessage((int)ClientPackets.connect, "0"));
        }

        public static string Disconnect()
        {
            return sendMessage((int)ClientPackets.disconnect, "0");
        }
    }
}
