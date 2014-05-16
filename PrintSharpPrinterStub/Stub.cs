using System;
using System.Net;
using System.Text;
using System.Threading;

namespace PrintSharpPrinterStub
{
    public class Ping : IDisposable
    {
        private readonly Thread _innerThread;
        private static IPEndPoint _distant;
        private static int _localPort;
        private static bool _listenerReady;

        public Ping(int serverPort, int printerPort)
        {
            _listenerReady = false;
            _innerThread = new Thread(Listen) {IsBackground = true};
            _distant = new IPEndPoint(IPAddress.Parse("127.0.0.1"), serverPort);
            _localPort = printerPort;
            _innerThread.Start();
            while(!_listenerReady) Thread.Sleep(1);
        }

        private static void Listen()
        {
            var expectedQuery = Encoding.UTF8.GetBytes("ping");
            var response = Encoding.UTF8.GetBytes("OK");

            SocketManager.SocketManager.RespondToGivenQuery(expectedQuery, response, _distant, _localPort);
        }

        public void Dispose()
        {
            _innerThread.Abort();
        }
    }
}
