
using System.Configuration;
using HttpServices;

namespace Server
{
    public class EWSServer : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var connStr = ConfigurationManager.ConnectionStrings["AzureDbConn"].ConnectionString;
            new AppHost(connStr).Init();
        }
    }
}