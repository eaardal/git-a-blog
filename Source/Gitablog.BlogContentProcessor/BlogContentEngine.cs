using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class BlogContentEngine
    {
        private readonly ContentLocator _contentLocator;
        private readonly ContentProcessor _contentProcessor;
        private readonly ContentRetriever _contentRetriever;

        public BlogContentEngine(ContentLocator contentLocator, ContentProcessor contentProcessor, ContentRetriever contentRetriever)
        {
            if (contentLocator == null) throw new ArgumentNullException("contentLocator");
            if (contentProcessor == null) throw new ArgumentNullException("contentProcessor");
            if (contentRetriever == null) throw new ArgumentNullException("contentRetriever");

            _contentLocator = contentLocator;
            _contentProcessor = contentProcessor;
            _contentRetriever = contentRetriever;
        }

        public async Task<IEnumerable<BlogEntry>> GetBlogContent()
        {
            var pollResults = await _contentLocator.Locate();

            var rawMarkdownContents = await _contentRetriever.Retrieve(pollResults);

            return await _contentProcessor.Process(rawMarkdownContents);
        }
    }
}
