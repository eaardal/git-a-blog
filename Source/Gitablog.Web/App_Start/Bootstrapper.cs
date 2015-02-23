using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Gitablog.BlogContentProcessor;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class Bootstrapper
    {
        public static IIoC Wire()
        {
            var builder = new ContainerBuilder();
            
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.Contains("Gitablog"));

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Except<IoC>()
                .Except<StateSynchronizer>()
                .Except<ContentState>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();

            builder.RegisterType<StateSynchronizer>().AsSelf().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ContentState>().AsSelf().AsImplementedInterfaces().SingleInstance();

            var container = builder.Build();

            var ioc = container.Resolve<IIoC>();
            ioc.RegisterContainer(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return ioc;
        }
    }
}