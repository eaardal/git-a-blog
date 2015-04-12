using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.BlogContentProcessor.Utils;
using HtmlAgilityPack;

namespace Gitablog.BlogContentProcessor
{
    public class BlogPostProcessor : IBlogPostProcessor
    {
        private readonly IMarkdownUtil _markdownUtil;

        public BlogPostProcessor(IMarkdownUtil markdownUtil)
        {
            if (markdownUtil == null) throw new ArgumentNullException("markdownUtil");
            _markdownUtil = markdownUtil;
        }

        public async Task<IEnumerable<PostDto>> Process(IEnumerable<RawMarkdownContent> rawMarkdownContents)
        {
            var posts = new List<PostDto>();

            await Task.Run(() =>
                {
                    foreach (var content in rawMarkdownContents)
                    {
                        var rawHtml = _markdownUtil.ParseToHtml(content.Content);

                        PostMetadata metadata;
                        var sanitizedHtml = ExtractAndRemoveMetadata(rawHtml, out metadata);

                        var post = new PostDto
                        {
                            RawHtml = sanitizedHtml,
                            Tags = metadata.Tags
                        };

                        try
                        {
                            SetCategory(post, content.FileUrl);
                            posts.Add(post);
                        }
                        catch (BlogContentValidationException ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                });

            return posts;
        }

        private string ExtractAndRemoveMetadata(string rawHtml, out PostMetadata metadata)
        {
            metadata = new PostMetadata();

            var document = new HtmlDocument();
            document.LoadHtml(rawHtml);

            var metadataNodes = document.DocumentNode.SelectNodes("//div[contains(@id,'metadata')]");

            if (metadataNodes == null || !metadataNodes.Any())
            {
                return rawHtml;
            }

            var metadataDiv = metadataNodes.FirstOrDefault();

            if (metadataDiv != null)
            {
                var tagsNodes = metadataDiv.SelectNodes("//div[contains(@id,'tags')]");

                if (tagsNodes != null && tagsNodes.Any())
                {
                    var tagsDiv = tagsNodes.FirstOrDefault();

                    if (tagsDiv != null)
                    {
                        var tags = tagsDiv.InnerText;
                        metadata.ParseTags(tags);

                        tagsDiv.Remove();
                    }    
                }

                metadataDiv.Remove();
            }

            return document.DocumentNode.OuterHtml;
        }

        private void SetCategory(PostDto post, string url)
        {
            var lastForwardSlash = url.LastIndexOf('/') + 1;

            var fileName = url.Substring(lastForwardSlash, url.Length - lastForwardSlash);

            var fileNameElements = fileName.Split('.');

            if (fileNameElements.Count() != 3)
            {
                throw new BlogContentValidationException("Could not parse file name in " + url);
            }

            post.Category = fileNameElements[0].ToLower();
        }
    }

    internal class PostMetadata
    {
        public IEnumerable<string> Tags { get; set; }

        public PostMetadata()
        {
            Tags = new List<string>();
        }

        public void ParseTags(string tags)
        {
            Tags = tags.Split(' ');
        }
    }
}