using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class StateSynchronizer : ITask
    {
        private readonly ContentState _state;
        private readonly BlogContentEngine _contentEngine;

        public StateSynchronizer(ContentState state, BlogContentEngine contentEngine) 
        {
            if (state == null) throw new ArgumentNullException("state");
            if (contentEngine == null) throw new ArgumentNullException("contentEngine");
            _state = state;
            _contentEngine = contentEngine;
        }

        public async Task<IDictionary<string, IEnumerable<BlogEntry>>> Synchronize()
        {
            Debug.WriteLine("Doing StateSynchronizer work");

            _state.State = await _contentEngine.GetBlogContent();

            return _state.State;
        }
        
        public void Execute()
        {
            Synchronize().Wait();
        }
    }
}
