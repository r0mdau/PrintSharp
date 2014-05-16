using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketManager
{
    public static class SocketManager
    {
        public static Receiver RespondToGivenQuery(byte[] expectedQuery, byte[] answer, EndPoint respondTo, int fromPort)
        {
            var receiver = new Receiver(fromPort, bytes => { if (bytes == expectedQuery) Send(respondTo, answer); });
            receiver.WaitIsReady();
            return receiver;
        }

        public static byte[] SendAndWaitForResponse(IPEndPoint distant, byte[] data, int localPort, int timeout = 0)
        {
            var hasReceived = false;
            var result = new byte[1024 * 5000];
            var receiver = new Receiver(localPort, bytes => { 
                result = bytes;
                hasReceived = true;
            });
            var expiration = DateTime.Now.AddSeconds(timeout);

            receiver.WaitIsReady();
            Send(distant, data);

            while(!hasReceived || expiration.CompareTo(DateTime.Now) <= 0) Thread.Sleep(10);
            receiver.Dispose();

            return result;
        }

        public static void Send(EndPoint distant, byte[] data)
        {
            var clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                clientSock.Connect(distant);
                clientSock.Send(data);
            }
            finally
            {
                clientSock.Close();
            }
        }

        public static bool TrySend(EndPoint distant, byte[] data, int timeout)
        {
            var expiration = DateTime.Now.AddSeconds(timeout);
            while (DateTime.Now.CompareTo(expiration) < 0)
                try
                {
                    Send(distant, data);
                    return true;
                }
                catch (SocketException)
                {
                }
                finally
                {
                    Thread.Sleep(10);
                }
            return false;
        }
    }
}
