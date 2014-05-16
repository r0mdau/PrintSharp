using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintSharpPrinterStub;
using PrintSharpServer;

namespace TestPrintSharpServer
{
    [TestClass]
    public class TestImprimante
    {
        [TestMethod]
        public void TestPeutSeCreerAvecIpEtPortDefaut()
        {
            CreerAvecIpEtPort();
        }

        [TestMethod]
        public void TestPeutSeCreerAvecUneIpEtUnPort()
        {
            CreerAvecIpEtPort("10.2.10.3", 5657);
        }

        [TestMethod]
        public void TestEstUniqueParIpEtPort()
        {
            Imprimante imprimante = CreerAvecIpEtPort("10.2.10.3", 5657);

            Assert.AreEqual(imprimante, CreerAvecIpEtPort("10.2.10.3", 5657));
            Assert.AreNotEqual(imprimante, CreerAvecIpEtPort("10.2.10.4", 5657));
            Assert.AreNotEqual(imprimante, CreerAvecIpEtPort("10.2.10.3", 5658));
        }

        [TestMethod]
        public void TestPeutPingUneImprimante()
        {
            var ping = new Ping(Server.DefaultPort, Imprimante.DefaultPort + 1);

            try
            {
                CreerAvecIpEtPort(Imprimante.DefaultIp, Imprimante.DefaultPort + 1).Ping();
            }
            finally
            {
                ping.Dispose();
            }
        }

        private static Imprimante CreerAvecIpEtPort(string ip = null, int port = 0)
        {
            Imprimante imprimante;
            if (ip != null)
            {
                if (port != 0)
                {
                    imprimante = new Imprimante(ip, port);
                    Assert.AreEqual(port, imprimante.Port);
                }
                else imprimante = new Imprimante(ip);

                Assert.AreEqual(ip, imprimante.Ip);
            }
            else
            {
                imprimante = new Imprimante();
                Assert.AreEqual(Imprimante.DefaultPort, imprimante.Port);
                Assert.AreEqual(Imprimante.DefaultIp, imprimante.Ip);
            }

            return imprimante;
        }
    }
}