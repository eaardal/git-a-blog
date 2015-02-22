using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor
{
    public class ContentProcessor
    {
        private readonly MarkdownUtil _markdownUtil;

        public ContentProcessor(MarkdownUtil markdownUtil)
        {
            if (markdownUtil == null) throw new ArgumentNullException("markdownUtil");
            _markdownUtil = markdownUtil;
        }

        public async Task<IEnumerable<BlogEntry>> ProcessRawContent(IEnumerable<RawContent> rawContents)
        {
            return await
                Task.Run(() => rawContents.Select(r => _markdownUtil.Convert(r.RawFileContent))
                    .Select(markdownContent => new BlogEntry {RawHtml = markdownContent}));
        }
    }
}