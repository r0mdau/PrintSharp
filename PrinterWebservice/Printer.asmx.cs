using System.ComponentModel;
using System.Web.Services;
using WebserviceAbstract;

namespace PrinterWebservice
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Printer : PrinterWebserviceAbstract
    {
        protected override IPrinter Handler
        {
            get { return new PrintSharpPrinter.Printer(); }
        }
    }
}