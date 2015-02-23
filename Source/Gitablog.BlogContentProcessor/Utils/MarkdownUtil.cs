using Gitablog.BlogContentProcessor.Abstract;
using MarkdownSharp;

namespace Gitablog.BlogContentProcessor.Utils
{
    public class MarkdownUtil : IMarkdownUtil
    {
        public string ParseToHtml(string rawMarkdown)
        {
            return new Markdown().Transform(rawMarkdown);
        }
    }
}
