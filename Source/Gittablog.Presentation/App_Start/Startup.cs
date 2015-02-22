using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Gittablog.Presentation.App_Start.Startup), "Configuration")]

namespace Gittablog.Presentation.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            ConfigureSignalR(builder);
        }

        public void ConfigureSignalR(IAppBuilder builder)
        {
            // For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888

            builder.MapSignalR();
        }
    }
}