namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IMarkdownUtil
    {
        string ParseToHtml(string rawMarkdown);
    }
}