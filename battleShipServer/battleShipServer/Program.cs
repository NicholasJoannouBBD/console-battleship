using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Dictionary<string, string> STATES = new Dictionary<string, string>()
{
    {"WAIT", "Waiting for all players to join"},
    {"SETUP", "Starting game setup..." },
    {"PLAY", "Now playing..." }
};
string state = STATES["WAIT"];
// Set up the server socket
TcpListener serverSocket = new(IPAddress.Loopback, 8888);
serverSocket.Start();
Console.WriteLine("Server started.");

// Create client sockets
TcpClient client1 = serverSocket.AcceptTcpClient();
Console.WriteLine("Client 1 connected.");
// Start listening for messages from clients
NetworkStream stream1 = client1.GetStream();
StreamReader readerPlayer1 = new StreamReader(stream1);
StreamWriter writerPlayer1 = new StreamWriter(stream1);

TcpClient client2 = serverSocket.AcceptTcpClient();
Console.WriteLine("Client 2 connected.");

NetworkStream stream2 = client2.GetStream();
StreamReader readerPlayer2 = new StreamReader(stream2);
StreamWriter writerPlayer2 = new StreamWriter(stream2);
writerPlayer1.WriteLine("Player 2 joined");

byte[] buffer1 = new byte[1024];
byte[] buffer2 = new byte[1024];

while (true)
{
    //Enter this loop once players have joined, now send response to both and the state must change
    if (stream1.CanRead && stream2.CanWrite && stream2.CanRead && stream2.CanWrite && state == STATES["WAIT"])
    {
        writerPlayer1.WriteLine("Ready");
        writerPlayer1.Flush();
        writerPlayer2.WriteLine("Ready");
        writerPlayer2.Flush();
        Console.WriteLine("Readying up");
        state = STATES["SETUP"];
    }


    // Read messages from client 1 and send to client 2
    if (stream1.DataAvailable)
    {
        int bytesRead = stream2.Read(buffer2, 0, buffer2.Length);
        string message = Encoding.ASCII.GetString(buffer2, 0, bytesRead);
        
        //writerPlayer2.WriteLine("MESSAGE TO P2");
    }

    // Read messages from client 2 and send to client 1
    if (stream2.DataAvailable)
    {
        int bytesRead = stream2.Read(buffer2, 0, buffer2.Length);
        string message = Encoding.ASCII.GetString(buffer2, 0, bytesRead);
       
        //stream1.Write(buffer2, 0, bytesRead);
    }

   
}

// Close connections
client1.Close();
client2.Close();
serverSocket.Stop();
