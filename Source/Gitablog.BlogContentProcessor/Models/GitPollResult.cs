using System;
using System.Collections.Generic;
using System.Linq;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor.Models
{
    public class GitPollResult : IGitPollResult
    {
        public bool IsNewCommit { get; set; }
        public DateTimeOffset? PushedTimestamp { get; set; }
        public string Identifier { get; set; }
        public IEnumerable<RemoteMarkdownFile> MarkdownFiles { get; set; }
        public bool Success
        {
            get
            {
                return !String.IsNullOrEmpty(Identifier) && PushedTimestamp.HasValue && MarkdownFiles.Any();
            }
        }

        public GitPollResult()
        {
            MarkdownFiles = new List<RemoteMarkdownFile>();
        }
    }
}