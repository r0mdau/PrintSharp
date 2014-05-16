using System;
using System.Net;
using System.Text;

namespace PrintSharpServer
{
    public class Imprimante : IDisposable
    {
        public const string DefaultIp = "127.0.0.1";
        public const int DefaultPort = 40300;
        private readonly IPEndPoint _endPoint;

        public string Ip { get { return _endPoint.Address.ToString(); } }
        public int Port { get { return _endPoint.Port; } }

        public Imprimante(string ipAdress = DefaultIp, int port = DefaultPort)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
        }

        private bool Equals(Imprimante other)
        {
            return Equals(_endPoint, other._endPoint);
        }

        public override int GetHashCode()
        {
            return (_endPoint != null ? _endPoint.GetHashCode() : 0);
        }

        public void Dispose()
        {
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == GetType() && Equals((Imprimante) obj);
        }

        public bool Ping()
        {
            var query = Encoding.UTF8.GetBytes("PING");
            return Encoding.UTF8.GetString(SocketManager.SocketManager.SendAndWaitForResponse(_endPoint, query, Server.DefaultPort, 10)) == "OK";
        }
    }
}
