using System.Collections.Generic;
using System.Linq;

namespace PrintSharpServer
{
    public class Server
    {
        public const int DefaultPort = 5656;

        public List<Imprimante> Imprimantes { get { return _imprimantes.Select(etatImprimante => etatImprimante.Imprimante).ToList(); } }
        private readonly List<EtatImprimante> _imprimantes = new List<EtatImprimante>(); 

        private class EtatImprimante
        {
            public Imprimante Imprimante { get; private set; }
            public bool Active;
            public EtatImprimante(Imprimante imprimante)
            {
                Active = false;
                Imprimante = imprimante;
            }
        }

        public static string Ping()
        {
            return "OK";
        }

        public Server(IEnumerable<Imprimante> imprimantes = null)
        {
            if (imprimantes == null) return;
            foreach (var imprimante in imprimantes) _imprimantes.Add(new EtatImprimante(imprimante));
        }

        public bool EstActive(Imprimante imprimante)
        {
            return _imprimantes.First(imp => imp.Imprimante.Equals(imprimante)).Active;
        }

        public void Activer(Imprimante imprimante)
        {
            var etat = _imprimantes.First(imp => imp.Imprimante.Equals(imprimante));
            etat.Active = true;
        }

        public void Desactiver(Imprimante imprimante)
        {
            var etat = _imprimantes.First(imp => imp.Imprimante.Equals(imprimante));
            etat.Active = false;
        }
    }
}
