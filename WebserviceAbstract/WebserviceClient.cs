using System.ServiceModel;
using WebserviceAbstract.DistantPrinter;

namespace WebserviceAbstract
{
    public class Client
    {
        private readonly PrinterSoapClient _client;

        public Client(string uri)
        {
            _client = new PrinterSoapClient(new BasicHttpBinding(), new EndpointAddress(uri));
        }

        public bool Ping()
        {
            return _client.Ping();
        }

        public int Print(int taille)
        {
            return _client.Print(taille, "", 1);
        }
        public string Status(int jobId)
        {
            return _client.Status(jobId);
        }
    }
}
