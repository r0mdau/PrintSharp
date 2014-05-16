using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketManager
{
    public class Receiver : IDisposable
    {
        private readonly Thread _receiverThread;
        private readonly ClientHandler[] _handlers;
        private bool _readyToReceive;

        public Receiver(int listenedPort, Action<byte[]> receivedDataHandler, int slots = 10)
        {
            _receiverThread = new Thread(() => StartReceiver(ref _readyToReceive, listenedPort, _handlers))
            {
                IsBackground = true
            };
            _receiverThread.Start();

            _handlers = ClientHandler.GetPool(receivedDataHandler, slots);
        }

        private static void StartReceiver(ref bool ready, int port, ClientHandler[] pool)
        {
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                server.Start();

                while (true)
                {
                    ready = true;
                    var client = server.AcceptTcpClient();
                    try
                    {
                        pool.First(element => element.Free).Handle(client);
                    }
                    catch (ArgumentNullException)
                    {
                        client.Close();
                    }
                    ready = false;
                }
            }
            finally
            {
                if (server != null) server.Stop();
            }
        }

        public void Dispose()
        {
            _readyToReceive = false;

            foreach (var clientHandler in _handlers)
            {
                clientHandler.Dispose();
            }

            _receiverThread.Abort();
        }

        public void WaitIsReady()
        {
            while (!_readyToReceive) Thread.Sleep(1);
        }
    }

    internal class ClientHandler : IDisposable
    {
        public bool Free { get; private set; }
        private readonly Thread _thread;
        private TcpClient _client;

        public static ClientHandler[] GetPool(Action<byte[]> receivedDataHandler, int number)
        {
            var handlers = new List<ClientHandler>();
            for(var i = 0; i < number; i++) handlers.Add(new ClientHandler(receivedDataHandler));
            return handlers.ToArray();
        }

        private ClientHandler(Action<byte[]> receivedDataHandler)
        {
            _thread = new Thread(() => DoAction(ref _client, receivedDataHandler, () => { Free = true; }))
            {
                IsBackground = true
            };

            Free = true;
        }

        private static void DoAction(ref TcpClient client, Action<byte[]> action, Action onFinish)
        {
            var bytes = new List<byte>();
            var networkStream = client.GetStream();
            var buffer = new byte[client.ReceiveBufferSize];

            while (networkStream.DataAvailable)
            {
                networkStream.Read(buffer, 0, client.ReceiveBufferSize);
                bytes.AddRange(buffer);
            }

            action.Invoke(bytes.ToArray());
            onFinish.Invoke();
        }

        public void Handle(TcpClient client)
        {
            if (!Free) return;
            Free = false;

            _client = client;
            _thread.Start();
        }

        public void Dispose()
        {
            Free = false;
            if(_thread != null) _thread.Abort();
            if(_client != null) _client.Close();
        }
    } 
}
