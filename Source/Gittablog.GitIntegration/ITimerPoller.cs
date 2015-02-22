using System;
using System.Threading.Tasks;

namespace Gittablog.GitIntegration
{
    public interface ITimerPoller
    {
        void Start(Func<Task> action, int interval = 60000);
        void Stop();
    }
}