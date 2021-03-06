﻿using System.ServiceModel;
using WebserviceClient.DistantPrinter;

namespace WebserviceClient
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

        public int Print(int taille, string nom, int copies)
        {
            return _client.Print(taille, nom, copies);
        }
        public string Status(int jobId)
        {
            return _client.Status(jobId);
        }
    }
}
