using System;
using System.Net;
using System.Text;
using System.Threading;

namespace PrintSharpPrinterStub
{
    public class Ping : IDisposable
    {
        private static IPEndPoint _distant;
        private static int _localPort;
        private static bool _listenerReady;
        private readonly Thread _innerThread;

        public Ping(int serverPort, int printerPort)
        {
            _listenerReady = false;
            _innerThread = new Thread(Listen) {IsBackground = true};
            _distant = new IPEndPoint(IPAddress.Parse("127.0.0.1"), serverPort);
            _localPort = printerPort;
            _innerThread.Start();
            while (!_listenerReady) Thread.Sleep(1);
        }

        public void Dispose()
        {
            _innerThread.Abort();
        }

        private static void Listen()
        {
            byte[] expectedQuery = Encoding.UTF8.GetBytes("ping");
            byte[] response = Encoding.UTF8.GetBytes("OK");

            SocketManager.SocketManager.RespondToGivenQuery(expectedQuery, response, _distant, _localPort);
        }
    }
}