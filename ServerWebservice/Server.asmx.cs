using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Services;
using PrintSharpServer;
using WebserviceAbstract;

namespace ServerWebservice
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Server : PrinterWebserviceAbstract
    {
        protected override IPrinter Handler
        {
            get { return ServerInternal.Instance(); }
        }

        private class ServerInternal : IPrinter
        {
            public const int DefaultPort = 5656;
            private static readonly ServerInternal _instance;

            private readonly List<EtatImprimante> _imprimantes = new List<EtatImprimante>();

            static ServerInternal()
            {
                _instance = new ServerInternal();
            }

            private ServerInternal(IEnumerable<Imprimante> imprimantes = null)
            {
                if (imprimantes == null) return;
                foreach (var imprimante in imprimantes) _imprimantes.Add(new EtatImprimante(imprimante));
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

            public static ServerInternal Instance()
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
}