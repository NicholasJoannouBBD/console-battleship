namespace Client
{
    // server to client
    public enum ServerPackets
    {
        usernameReceived = 1
    }

    // client to server
    public enum ClientPackets
    {
        username = 1,
        xCoords = 2,
        yCoords = 3,
        connect = 4,
        disconnect = 5
    }
}
