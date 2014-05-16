using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace PrintSharpClient
{
    static class FileTransfert
    {
        public static void SendFile(String data)
        {
            try
            {
                 var ipAddress = Dns.GetHostAddresses("192.168.2.12");
                 var ipEnd = new IPEndPoint(ipAddress[0], 5656);
                 var clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                 var fileNameByte = Encoding.ASCII.GetBytes(data);
                 Console.WriteLine(@"Buffering ...");
                 Console.WriteLine(@"Connection to server ...");
                 clientSock.Connect(ipEnd);
                 clientSock.Send(fileNameByte);
                 clientSock.Close();
            }
            catch (Exception ex)
            {
                 if(ex.Message==@"No connection could be made because the target machine actively refused it")
                     Console.WriteLine(@"File Sending fail. Because server not running.");
                 else
                     Console.WriteLine(@"File Sending fail." + ex.Message);
            } 
        }
    }
}
