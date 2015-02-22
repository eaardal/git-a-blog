using MarkdownSharp;

namespace Gitablog.BlogContentProcessor.Utils
{
    public class MarkdownUtil
    {
        public string ParseToHtml(string rawMarkdown)
        {
            return new Markdown().Transform(rawMarkdown);
        }
    }
}
