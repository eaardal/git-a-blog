using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.BlogContentProcessor.Utils;

namespace Gitablog.BlogContentProcessor
{
    public class ContentRetriever
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly MarkdownUtil _markdownUtil;

        public ContentRetriever(IFileDownloader fileDownloader, MarkdownUtil markdownUtil)
        {
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            if (markdownUtil == null) throw new ArgumentNullException("markdownUtil");

            _fileDownloader = fileDownloader;
            _markdownUtil = markdownUtil;
        }

        public async Task<IEnumerable<RawMarkdownContent>> Retrieve(IEnumerable<IRawContent> rawContents)
        {
            var blogEntries = new List<RawMarkdownContent>();

            foreach (var rawContent in rawContents)
            {
                foreach (var file in rawContent.MarkdownFiles)
                {
                    var content = await _fileDownloader.Download(file.Url);

                    blogEntries.Add(new RawMarkdownContent{ Content = content });
                }
            }

            return blogEntries;
        }
    }
}
