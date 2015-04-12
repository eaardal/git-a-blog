using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.Infrastructure;

namespace Gitablog.BlogContentProcessor
{
    public class ContentState : IContentState
    {
        private readonly IIoC _ioc;

        public delegate void StateUpdatedEventHandler();

        public event StateUpdatedEventHandler StateUpdated;

        public IDictionary<string, IEnumerable<PostDto>> State { get; set; }

        public bool HasState { get { return State != null && State.Keys.Any(); } }

        public ContentState(IIoC ioc)
        {
            if (ioc == null) throw new ArgumentNullException("ioc");
            _ioc = ioc;
        }

        public async Task<IDictionary<string, IEnumerable<PostDto>>> RequestStateUpdate()
        {
            var sync = _ioc.Resolve<IStateSynchronizer>();

            State = await sync.Synchronize();

            return State;
        }
    }
}
