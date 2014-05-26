using System.Web.Services;
using WebserviceAbstract;

namespace ServerWebservice
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Server : PrinterWebserviceAbstract
    {
        protected override IPrinter Handler
        {
            get { return PrintSharpServer.Server.Instance(); }
        }
    }
}
