using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor
{
    public class ContentLocator : IContentLocator
    {
        private readonly IEnumerable<IGitContentLocatorStrategy> _gitContentLocatorStrategies;

        public ContentLocator(IEnumerable<IGitContentLocatorStrategy> gitContentLocatorStrategies)
        {
            if (gitContentLocatorStrategies == null) throw new ArgumentNullException("gitContentLocatorStrategies");
            _gitContentLocatorStrategies = gitContentLocatorStrategies;
        }

        public async Task<IEnumerable<IGitPollResult>> Locate()
        {
            var content = new List<IGitPollResult>();

            foreach (var strategy in _gitContentLocatorStrategies)
            {
                content.Add(await strategy.LocateContent());
            }

            return content;
        }
    }
}
