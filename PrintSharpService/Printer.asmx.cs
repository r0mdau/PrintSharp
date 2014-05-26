using System.Web.Services;

namespace PrintSharpService
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public abstract class Printer : WebService
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
