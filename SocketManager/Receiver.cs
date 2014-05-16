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
        private readonly ClientHandler[] _handlers;
        private readonly Thread _receiverThread;
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

        public void Dispose()
        {
            _readyToReceive = false;

            foreach (ClientHandler clientHandler in _handlers)
            {
                clientHandler.Dispose();
            }

            _receiverThread.Abort();
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
                    TcpClient client = server.AcceptTcpClient();
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

        public void WaitIsReady()
        {
            while (!_readyToReceive) Thread.Sleep(1);
        }
    }

    internal class ClientHandler : IDisposable
    {
        private readonly Thread _thread;
        private TcpClient _client;

        private ClientHandler(Action<byte[]> receivedDataHandler)
        {
            _thread = new Thread(() => DoAction(ref _client, receivedDataHandler, () => { Free = true; }))
            {
                IsBackground = true
            };

            Free = true;
        }

        public bool Free { get; private set; }

        public void Dispose()
        {
            Free = false;
            if (_thread != null) _thread.Abort();
            if (_client != null) _client.Close();
        }

        public static ClientHandler[] GetPool(Action<byte[]> receivedDataHandler, int number)
        {
            var handlers = new List<ClientHandler>();
            for (int i = 0; i < number; i++) handlers.Add(new ClientHandler(receivedDataHandler));
            return handlers.ToArray();
        }

        private static void DoAction(ref TcpClient client, Action<byte[]> action, Action onFinish)
        {
            var bytes = new List<byte>();
            NetworkStream networkStream = client.GetStream();
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
    }
}