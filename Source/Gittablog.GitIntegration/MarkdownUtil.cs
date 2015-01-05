using MarkdownSharp;

namespace Gittablog.GitIntegration
{
    public class MarkdownUtil
    {
        public string Convert(string raw)
        {
            var mark = new Markdown();
            return mark.Transform(raw);
        }
    }
}
