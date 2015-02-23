using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class BlogContentEngine : IBlogContentEngine
    {
        private readonly ContentLocator _contentLocator;
        private readonly ContentProcessor _contentProcessor;
        private readonly ContentRetriever _contentRetriever;
        private readonly BlogLayoutOrganizer _layoutOrganizer;

        public BlogContentEngine(ContentLocator contentLocator, ContentProcessor contentProcessor, ContentRetriever contentRetriever, BlogLayoutOrganizer layoutOrganizer)
        {
            if (contentLocator == null) throw new ArgumentNullException("contentLocator");
            if (contentProcessor == null) throw new ArgumentNullException("contentProcessor");
            if (contentRetriever == null) throw new ArgumentNullException("contentRetriever");
            if (layoutOrganizer == null) throw new ArgumentNullException("layoutOrganizer");

            _contentLocator = contentLocator;
            _contentProcessor = contentProcessor;
            _contentRetriever = contentRetriever;
            _layoutOrganizer = layoutOrganizer;
        }

        public async Task<IDictionary<string, IEnumerable<BlogEntry>>> GetBlogContent()
        {
            var rawContents = await _contentLocator.Locate();

            var rawMarkdownContents = await _contentRetriever.Retrieve(rawContents);

            var blogEntries = await _contentProcessor.Process(rawMarkdownContents);

            var organized = _layoutOrganizer.Organize(blogEntries);

            return organized;
        }
    }
}
