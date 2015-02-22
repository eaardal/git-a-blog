using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.BlogContentProcessor.Utils;

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

        public async Task<IEnumerable<BlogEntry>> Process(IEnumerable<RawMarkdownContent> rawContents)
        {
            return await
                Task.Run(() => rawContents.Select(r => _markdownUtil.ParseToHtml(r.Content))
                    .Select(markdownContent => new BlogEntry {RawHtml = markdownContent}));
        }
    }
}