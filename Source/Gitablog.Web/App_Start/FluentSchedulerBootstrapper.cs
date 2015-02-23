using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;
using Gitablog.BlogContentProcessor;

namespace Gitablog.Web.App_Start
{
    public class FluentSchedulerBootstrapper : Registry
    {
        public FluentSchedulerBootstrapper()
        {
#if DEBUG
            Schedule<StateSynchronizer>().ToRunNow().AndEvery(1).Minutes();
#else
            Schedule<StateSynchronizer>().ToRunNow().AndEvery(1).Hours();
#endif
        }
    }
}