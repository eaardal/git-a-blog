using System;
using System.Threading.Tasks;
using System.Timers;

namespace Gittablog.GitIntegration
{
    public class TempTimerPoller : ITimerPoller
    {
        private Timer _timer;

        public void Start(Func<Task> action, int interval = 60000)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += async (sender, args) => await action();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
