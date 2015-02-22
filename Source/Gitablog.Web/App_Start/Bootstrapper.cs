using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class Bootstrapper
    {
        public static IIoC Wire()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Except<IoC>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<IoC>().As<IIoC>().SingleInstance();

            var container = builder.Build();

            var ioc = container.Resolve<IIoC>();
            ioc.RegisterContainer(container);

            return ioc;
        }
    }
}