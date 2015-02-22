using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor
{
    class ContentProcessor
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly MarkdownUtil _markdownUtil;

        public ContentProcessor(IFileDownloader fileDownloader, MarkdownUtil markdownUtil)
        {
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            if (markdownUtil == null) throw new ArgumentNullException("markdownUtil");

            _fileDownloader = fileDownloader;
            _markdownUtil = markdownUtil;
        }

        public async Task<IEnumerable<BlogEntry>> ProcessRawContent(IEnumerable<IPollResult> pollResults)
        {
            var blogEntries = new List<BlogEntry>();

            foreach (var pollResult in pollResults)
            {
                foreach (var file in pollResult.MarkdownFiles)
                {
                    var content = await _fileDownloader.Download(file.Url);

                    var markdown = _markdownUtil.Convert(content);

                    var entry = new BlogEntry
                    {
                        RawHtml = markdown
                    };

                    blogEntries.Add(entry);
                }
            }

            return blogEntries;
        } 
    }
}
