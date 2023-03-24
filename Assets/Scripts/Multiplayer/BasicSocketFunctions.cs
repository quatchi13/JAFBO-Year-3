using System;
using System.Net;
using System.Net.Sockets;


namespace JAFnetwork
{
    public static class SockFunctions
    {
        public static bool HasStuffToRead(Socket s)
        {
            return s.Poll(1, SelectMode.SelectRead);
        }
        public static bool CanSend(Socket s)
        {
            return s.Poll(1, SelectMode.SelectWrite);
        }

        public static void KillClient(Socket s)
        {
            s.Shutdown(SocketShutdown.Both);
            s.Close();
            UnityEngine.Debug.Log("Successfully disconnected from server");
        }
    }

}
