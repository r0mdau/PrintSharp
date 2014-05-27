using System;
using System.ComponentModel;
using System.Web.Services;
using WebserviceAbstract;

namespace ServerWebservice
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Server : PrinterWebserviceAbstract<Server.ServerInternal>
    {
        public class ServerInternal : PrinterAbstract<ServerInternal>
        {
            protected override void TraiterQueue()
            {
                throw new NotImplementedException();
            }
        }
    }
}