using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class ContentRetriever : IContentRetriever
    {
        private readonly IFileDownloader _fileDownloader;

        public ContentRetriever(IFileDownloader fileDownloader)
        {
            if (fileDownloader == null) throw new ArgumentNullException("fileDownloader");
            _fileDownloader = fileDownloader;
        }

        public async Task<IEnumerable<RawMarkdownContent>> Retrieve(IEnumerable<IGitPollResult> rawContents)
        {
            var blogEntries = new List<RawMarkdownContent>();

            foreach (var rawContent in rawContents)
            {
                foreach (var file in rawContent.MarkdownFiles)
                {
                    var content = await _fileDownloader.Download(file.Url);

                    blogEntries.Add(new RawMarkdownContent{ Content = content, FileUrl = file.Url});
                }
            }

            return blogEntries;
        }
    }
}
