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

            var assemblies =
                Assembly.GetExecutingAssembly()
                    .GetReferencedAssemblies()
                    .Where(a => a.FullName.Contains("Gitablog"))
                    .Select(Assembly.Load)
                    .ToList();

            assemblies.Add(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Except<IoC>()
                .Except<StateSynchronizer>()
                .Except<ContentState>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterSingleton<IoC>();
            builder.RegisterSingleton<StateSynchronizer>();
            builder.RegisterSingleton<ContentState>();

            builder.RegisterType<FluentSchedulerTaskFactory>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<FluentSchedulerBootstrapper>().AsSelf().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}