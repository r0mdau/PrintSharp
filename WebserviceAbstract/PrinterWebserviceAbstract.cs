using System.Web.Services;

namespace WebserviceAbstract
{
    public abstract class PrinterWebserviceAbstract : WebService
    {
        protected abstract IPrinter Handler { get; }

        [WebMethod]
        public bool Ping()
        {
            return Handler.Ping();
        }

        [WebMethod]
        public int Print(int taille, string nom, int copies)
        {
            return Handler.Print(taille, nom, copies);
        }

        [WebMethod]
        public string Status(int jobId)
        {
            return Handler.Status(jobId);
        }
    }
}
