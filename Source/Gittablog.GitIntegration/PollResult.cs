using System;
using System.Collections.Generic;
using System.Linq;

namespace Gittablog.GitIntegration
{
    public class PollResult : IPollResult
    {
        public bool IsNewCommit { get; set; }
        public DateTimeOffset? PushedTimestamp { get; set; }
        public string Sha { get; set; }
        public List<string> MarkdownFiles { get; set; }
        public bool Success
        {
            get
            {
                return !String.IsNullOrEmpty(Sha) && PushedTimestamp.HasValue && MarkdownFiles.Any();
            }
        }

        public PollResult()
        {
            MarkdownFiles = new List<string>();
        }
    }
}