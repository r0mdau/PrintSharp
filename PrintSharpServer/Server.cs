using System;
using System.Collections.Generic;
using System.Linq;
using WebserviceAbstract;

namespace PrintSharpServer
{
    public class Server : IPrinter
    {
        public const int DefaultPort = 5656;
        private static readonly Server _instance;

        private readonly List<EtatImprimante> _imprimantes = new List<EtatImprimante>();

        static Server()
        {
            _instance = new Server();
        }

        private Server(IEnumerable<Imprimante> imprimantes = null)
        {
            if (imprimantes == null) return;
            foreach (Imprimante imprimante in imprimantes) _imprimantes.Add(new EtatImprimante(imprimante));
        }

        public List<Imprimante> Imprimantes
        {
            get { return _imprimantes.Select(etatImprimante => etatImprimante.Imprimante).ToList(); }
        }

        public string Status(int jobId)
        {
            throw new NotImplementedException();
        }

        public int Print(int taille, string nom, int copies)
        {
            throw new NotImplementedException();
        }

        bool IPrinter.Ping()
        {
            throw new NotImplementedException();
        }

        public static Server Instance()
        {
            return _instance;
        }

        public static string Ping()
        {
            return "OK";
        }

        public bool EstActive(Imprimante imprimante)
        {
            return _imprimantes.First(imp => imp.Imprimante.Equals(imprimante)).Active;
        }

        public void Activer(Imprimante imprimante)
        {
            EtatImprimante etat = _imprimantes.First(imp => imp.Imprimante.Equals(imprimante));
            etat.Active = true;
        }

        public void Desactiver(Imprimante imprimante)
        {
            EtatImprimante etat = _imprimantes.First(imp => imp.Imprimante.Equals(imprimante));
            etat.Active = false;
        }

        private class EtatImprimante
        {
            public bool Active;

            public EtatImprimante(Imprimante imprimante)
            {
                Active = false;
                Imprimante = imprimante;
            }

            public Imprimante Imprimante { get; private set; }
        }
    }
}