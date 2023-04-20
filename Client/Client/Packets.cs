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
        username = 1
    }

    public static class ClientConstants
    {
        public const string username = "1";
    }

    public static class ServerConstants
    {
        public const string usernameReceived = "1";
    }
}
