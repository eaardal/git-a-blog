using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class StateSynchronizer : ITask, IStateSynchronizer
    {
        private readonly IContentState _state;
        private readonly IBlogContentEngine _contentEngine;

        public StateSynchronizer(IContentState state, IBlogContentEngine contentEngine) 
        {
            if (state == null) throw new ArgumentNullException("state");
            if (contentEngine == null) throw new ArgumentNullException("contentEngine");
            _state = state;
            _contentEngine = contentEngine;
        }

        public async Task<IDictionary<string, IEnumerable<PostDto>>> Synchronize()
        {
            _state.State = await _contentEngine.GetBlogContent();

            return _state.State;
        }
        
        public void Execute()
        {
            Synchronize().Wait();
        }
    }
}
