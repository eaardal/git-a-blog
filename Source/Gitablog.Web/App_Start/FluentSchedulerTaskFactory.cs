using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;
using Gitablog.Infrastructure;

namespace Gitablog.Web.App_Start
{
    public class FluentSchedulerTaskFactory : ITaskFactory
    {
        private readonly IIoC _ioc;

        public FluentSchedulerTaskFactory(IIoC ioc)
        {
            if (ioc == null) throw new ArgumentNullException("ioc");
            _ioc = ioc;
        }

        public ITask GetTaskInstance<T>() where T : ITask
        {
            return _ioc.Resolve<T>();
        }
    }
}