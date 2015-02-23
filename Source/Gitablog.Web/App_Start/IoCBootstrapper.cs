using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class IoCBootstrapper
    {
        public static IIoC ConfigureIoC()
        {
            var container = AutofacBootstrapper.WireDependencies();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var ioc = container.Resolve<IIoC>();
            ioc.RegisterContainer(container);

            return ioc;
        }
    }
}