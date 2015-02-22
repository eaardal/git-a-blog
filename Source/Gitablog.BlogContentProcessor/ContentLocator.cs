using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor
{
    public class ContentLocator
    {
        private readonly IGitContentLocatorStrategy _gitHubContentLocatorStrategy;

        public ContentLocator(IGitContentLocatorStrategy gitHubContentLocatorStrategy)
        {
            if (gitHubContentLocatorStrategy == null) throw new ArgumentNullException("gitHubContentLocatorStrategy");

            _gitHubContentLocatorStrategy = gitHubContentLocatorStrategy;
        }

        public async Task<IEnumerable<IRawContent>> Locate()
        {
            return new List<IRawContent>
            {
                await _gitHubContentLocatorStrategy.LocateContent()
            };
        }
    }
}
