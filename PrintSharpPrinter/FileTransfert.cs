using System;
using System.Net;
using System.Net.Sockets;


namespace PrintSharpPrinter
{
    class FileTransfert
    {
        //FILE TRANSFER USING C#.NET SOCKET - SERVER
                
        IPEndPoint ipEnd;
        Socket sock;
        public FileTransfert()
        {
            ipEnd = new IPEndPoint(IPAddress.Any, 5656);
            //Make IP end point to accept any IP address with port no 5656.
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //Here creating new socket object with protocol type and transfer data type
            sock.Bind(ipEnd);
            //Bind end point with newly created socket.
        }
        public static string curMsg = "Stopped";
        public void StartServer()
        {
            try
            {
                Console.WriteLine("Starting...");
                sock.Listen(100);
                /* That socket object can handle maximum 100 client connection at a time & 
                waiting for new client connection */
                curMsg = "Running and waiting to receive file.";
                Socket clientSock = sock.Accept();
                /* When request comes from client that accept it and return 
                new socket object for handle that client. */
                byte[] clientData = new byte[1024 * 5000];
                int receivedBytesLen = clientSock.Receive(clientData);
                string result = System.Text.Encoding.UTF8.GetString(clientData);
                Console.WriteLine("Receiving data..." + result);          
                clientSock.Close();
                /* Close binary writer and client socket */
                Console.WriteLine("Received; Server Stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("File Receiving error.");
            }
        }   
    }
}
