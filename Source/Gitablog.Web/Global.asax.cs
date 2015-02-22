using System.Web;
using System.Web.Routing;
using Gitablog.Web.App_Start;

namespace Gitablog.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Wire();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
