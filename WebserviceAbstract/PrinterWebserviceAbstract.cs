using System.Web.Services;

namespace WebserviceAbstract
{
    public abstract class PrinterWebserviceAbstract<T> : WebService where T : PrinterAbstract<T>, new()
    {

        [WebMethod]
        public bool Ping()
        {
            return true;
        }

        [WebMethod]
        public int Print(int taille, string nom, int copies)
        {
            return PrinterAbstract<T>.Print(taille);
        }

        [WebMethod]
        public string Status(int jobId)
        {
            return PrinterAbstract<T>.Instance.Status(jobId);
        }
    }
}