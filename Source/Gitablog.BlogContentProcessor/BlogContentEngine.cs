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
        private readonly IContentLocator _contentLocator;
        private readonly IBlogPostProcessor _blogPostProcessor;
        private readonly IContentRetriever _contentRetriever;
        private readonly IBlogLayoutOrganizer _layoutOrganizer;

        public BlogContentEngine(IContentLocator contentLocator, IBlogPostProcessor blogPostProcessor, IContentRetriever contentRetriever, IBlogLayoutOrganizer layoutOrganizer)
        {
            if (contentLocator == null) throw new ArgumentNullException("contentLocator");
            if (blogPostProcessor == null) throw new ArgumentNullException("blogPostProcessor");
            if (contentRetriever == null) throw new ArgumentNullException("contentRetriever");
            if (layoutOrganizer == null) throw new ArgumentNullException("layoutOrganizer");

            _contentLocator = contentLocator;
            _blogPostProcessor = blogPostProcessor;
            _contentRetriever = contentRetriever;
            _layoutOrganizer = layoutOrganizer;
        }

        public async Task<IDictionary<string, IEnumerable<PostDto>>> GetBlogContent()
        {
            var rawContents = await _contentLocator.Locate();

            var rawMarkdownContents = await _contentRetriever.Retrieve(rawContents);

            var blogEntries = await _blogPostProcessor.Process(rawMarkdownContents);

            var organized = _layoutOrganizer.Organize(blogEntries);

            return organized;
        }
    }
}
