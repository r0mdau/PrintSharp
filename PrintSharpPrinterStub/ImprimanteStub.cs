using System;
using PrintSharpServer;

namespace PrintSharpPrinterStub
{
    public class ImprimanteStub : Imprimante, IDisposable
    {
        private readonly Ping _ping;

        public ImprimanteStub(int port = DefaultPort) : base("127.0.0.1", port)
        {
            _ping = new Ping(Server.DefaultPort, port);
        }

        public new void Dispose()
        {
            _ping.Dispose();
        }
    }
}