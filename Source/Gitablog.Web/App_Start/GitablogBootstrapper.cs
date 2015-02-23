using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using FluentScheduler;
using Gitablog.BlogContentProcessor;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class GitablogBootstrapper
    {
        public static IIoC Wire()
        {
            var ioc = IoCBootstrapper.ConfigureIoC();

            TaskManager.TaskFactory = ioc.Resolve<FluentSchedulerTaskFactory>();
            TaskManager.Initialize(ioc.Resolve<FluentSchedulerBootstrapper>());

            return ioc;
        }
    }
}