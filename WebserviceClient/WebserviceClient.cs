using System.ServiceModel;
using WebserviceClient.DistantPrinter;

namespace WebserviceClient
{
    public class Client
    {
        private readonly PrinterSoapClient _client;

        public Client(string uri)
        {
            _client = new PrinterSoapClient(new BasicHttpBinding("PrinterSoap"), new EndpointAddress(uri));
        }

        private bool Ping()
        {
            return _client.Ping();
        }

        int Print(int taille, string nom, int copies)
        {
            return _client.Print(taille, nom, copies);
        }
        string Status(int jobId)
        {
            return _client.Status(jobId);
        }
    }
}
