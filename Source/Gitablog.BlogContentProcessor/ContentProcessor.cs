using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
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

        public async Task<IEnumerable<BlogEntry>> Process(IEnumerable<RawMarkdownContent> rawMarkdownContents)
        {
            var blogEntries = new List<BlogEntry>();

            await Task.Run(() =>
                {
                    foreach (var content in rawMarkdownContents)
                    {
                        var entry = new BlogEntry
                        {
                            RawHtml = _markdownUtil.ParseToHtml(content.Content)
                        };

                        try
                        {
                            SetCategory(entry, content.FileUrl);
                            blogEntries.Add(entry);
                        }
                        catch (BlogContentValidationException ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                });

            return blogEntries;
        }

        private void SetCategory(BlogEntry entry, string url)
        {
            var lastForwardSlash = url.LastIndexOf('/') + 1;

            var fileName = url.Substring(lastForwardSlash, url.Length - lastForwardSlash);

            var fileNameElements = fileName.Split('.');

            if (fileNameElements.Count() != 3)
            {
                throw new BlogContentValidationException("Could not parse file name in " + url);
            }

            entry.Category = fileNameElements[0];
        }
    }
}