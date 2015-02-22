using System;
using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface ITimerPoller
    {
        void Start(Func<Task> action, int interval = 60000);
        void Stop();
    }
}