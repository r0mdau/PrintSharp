using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketManager;

namespace TestSocketManager
{
    [TestClass]
    public class TestSocketManager
    {
        [TestMethod]
        public void TestSendAndReceive()
        {
            Assert.IsTrue(Receive(Send, 5));
        }

        [TestMethod]
        public void TestSendAndWaitForResponse()
        {
            var query = new byte[] { 0, 1 };
            var response = new byte[] { 2, 0 };
            var realResponse = new byte[0];

            var thr = new Thread(() =>
            {
                realResponse = SocketManager.SocketManager.SendAndWaitForResponse(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40560),
                query, 40561, 30);
            });

            var sendThr = new Thread(() => SocketManager.SocketManager.TrySend(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40561), response, 10));

            thr.Start();
            sendThr.Start();

            thr.Join(30000);

            Assert.AreEqual(0, realResponse.Except(response).Count());
        }

        [TestMethod]
        public void TestAutoSendAndReceive()
        {
            var query = new byte[] { 0, 1 };
            var response = new byte[] { 2, 0 };
            var realResponse = new byte[0];

            var thr = new Thread(() =>
            {
                realResponse = SocketManager.SocketManager.SendAndWaitForResponse(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40560),
                query, 40561, 30);
            });

            var sendThr = new Thread(() => SocketManager.SocketManager.RespondToGivenQuery(query, response, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40561), 41560));

            sendThr.Start();
            thr.Start();

            thr.Join(30000);

            Assert.AreEqual(0, realResponse.Except(response).Count());
        }

        private static bool Receive(Action action, int timeout)
        {
            var received = false;
            var receiver = new Receiver(40560, bytes => received = true);
            receiver.WaitIsReady();

            var expiration = DateTime.Now.AddSeconds(timeout);
            try
            {
                action.Invoke();
                while(expiration.CompareTo(DateTime.Now) > 0 && !received) Thread.Sleep(1);
                return received;
            }
            finally
            {
                receiver.Dispose();
            }
        }

        private static void Send()
        {
            SocketManager.SocketManager.Send(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40560), new byte[]{0, 1});
        }
    }
}
