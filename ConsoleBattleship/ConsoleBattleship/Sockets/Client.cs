﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ConsoleBattleship.Sockets
{
    internal class Client
    {
        TcpClient clientSocket = null;
        NetworkStream networkStream = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        public Client() { }
        public void ConnectClient()
        {
            this.clientSocket = new TcpClient();
            clientSocket.Connect("127.0.0.1", 8888);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Connected to server             ");
            Console.WriteLine("Waiting for players...");
            this.networkStream = clientSocket.GetStream();
            this.reader = new StreamReader(networkStream);
            this.writer = new StreamWriter(networkStream);
            networkStream.Flush();
        }
        public string ListenerClient()
        {
            try
            {
                
                if(clientSocket != null)
                {
                    string response = reader?.ReadLine() ?? "";
                    if (response.Contains("Ready"))
                    {
                        //change state to the game setup
                        return "Ready";
                    }
                    if (response.Contains("PLAY"))
                    {
                        return "PLAY";
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine("Connect the server first");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
            return String.Empty;
        }
        public void Setup()
        {
            networkStream.Write(Encoding.UTF8.GetBytes("SETUP Complete"), 0, Encoding.UTF8.GetBytes("SETUP Complete").Length);
        }
        public void Exit()
        {
            networkStream.Write(Encoding.UTF8.GetBytes("READY"), 0, Encoding.UTF8.GetBytes("READY").Length);
        }
        public override string ToString()
        {
            return "Client Socket";
        }
    }
}
