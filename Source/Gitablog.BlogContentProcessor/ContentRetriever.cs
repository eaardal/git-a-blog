using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor
{
    public class ContentRetriever
    {
        private readonly IGitPollerStrategy _gitHubPollerStrategy;

        public ContentRetriever(IGitPollerStrategy gitHubPollerStrategy)
        {
            if (gitHubPollerStrategy == null) throw new ArgumentNullException("gitHubPollerStrategy");

            _gitHubPollerStrategy = gitHubPollerStrategy;
        }

        public async Task<IEnumerable<IPollResult>> GetRawContent()
        {
            return new List<IPollResult>
            {
                await _gitHubPollerStrategy.Poll()
            };
        }
    }
}
