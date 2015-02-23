using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using Autofac;
using Gitablog.BlogContentProcessor;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class AutofacBootstrapper
    {
        public static IContainer WireDependencies()
        {
            var builder = new ContainerBuilder();

            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.Contains("Gitablog"));

            var assemblies =
                Assembly.GetExecutingAssembly()
                    .GetReferencedAssemblies()
                    .Where(a => a.FullName.Contains("Gitablog"))
                    .Select(Assembly.Load)
                    .ToArray();

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Except<IoC>()
                .Except<StateSynchronizer>()
                .Except<ContentState>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();

            builder.RegisterType<StateSynchronizer>().AsSelf().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ContentState>().AsSelf().AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}