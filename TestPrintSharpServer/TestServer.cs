using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintSharpPrinterStub;
using PrintSharpServer;

namespace TestPrintSharpServer
{
    [TestClass]
    public class TestServer
    {
        [TestMethod]
        public void TestRenvoieOkAUnPing()
        {
            Assert.AreEqual("OK", Server.Ping());
        }

        [TestMethod]
        public void TestPeutActiverUneImprimanteJoignable()
        {
            Dictionary<string, Imprimante> list = CreerListeImprimantesTest();
            ChangerEtatActivation(CreerServerAvecListeImprimante(list.Values.ToList()), list["Joignable"], true);
            try
            {
                ChangerEtatActivation(CreerServerAvecListeImprimante(list.Values.ToList()), list["Injoignable"], true);
                Assert.Fail("Le serveur accepte d'activer l'imprimante injoignable");
            }
            catch (AssertFailedException)
            {
            }
        }

        [TestMethod]
        public void TestPeutDesactiverUneImprimante()
        {
            Dictionary<string, Imprimante> list = CreerListeImprimantesTest();
            ChangerEtatActivation(CreerServerAvecListeImprimante(list.Values.ToList()), list["Joignable"], false);
        }

        private static void ChangerEtatActivation(Server server, Imprimante aChanger, bool activer)
        {
            List<bool> etatsInitiaux = server.Imprimantes.Select(server.EstActive).ToList();
            if (activer) server.Activer(aChanger);
            else server.Desactiver(aChanger);
            List<bool> etatsFinaux = server.Imprimantes.Select(server.EstActive).ToList();

            Assert.AreEqual(activer, server.EstActive(aChanger));
            Assert.IsTrue(etatsInitiaux.Except(etatsFinaux).Count() <= 1);
        }

        private static Server CreerServerAvecListeImprimante(List<Imprimante> imprimantes = null)
        {
            var srv = new Server(imprimantes);
            Assert.AreEqual(0, imprimantes != null ? imprimantes.Except(srv.Imprimantes).Count() : srv.Imprimantes.Count);

            foreach (Imprimante imprimante in srv.Imprimantes) Assert.IsFalse(srv.EstActive(imprimante));

            return srv;
        }

        private static Dictionary<string, Imprimante> CreerListeImprimantesTest()
        {
            var dictionnary = new Dictionary<string, Imprimante>
            {
                {"Joignable", new ImprimanteStub()},
                {"Injoignable", new Imprimante()}
            };
            try
            {
                Assert.IsTrue(dictionnary["Joignable"].Ping());
                Assert.IsFalse(dictionnary["Injoignable"].Ping());
            }
            finally
            {
                dictionnary["Joignable"].Dispose();
                dictionnary["Injoignable"].Dispose();
            }

            return dictionnary;
        }
    }
}