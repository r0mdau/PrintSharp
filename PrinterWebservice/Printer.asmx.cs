using System.Web.Services;
using WebserviceAbstract;

namespace PrinterWebservice
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Printer : PrinterWebserviceAbstract
    {
        protected override IPrinter Handler
        {
            get { return PrintSharpPrinter.Printer.Instance(); }
        }
    }
}
