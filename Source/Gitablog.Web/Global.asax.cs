﻿using System.Web;
using System.Web.Routing;
using FluentScheduler;
using Gitablog.BlogContentProcessor;
using Gitablog.Infrastructure;
using Gitablog.Web.App_Start;

namespace Gitablog.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var ioc = Bootstrapper.Wire();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            TaskManager.TaskFactory = ioc.Resolve<FluentSchedulerTaskFactory>();
            TaskManager.Initialize(ioc.Resolve<FluentSchedulerBootstrapper>());
        }
    }
}
