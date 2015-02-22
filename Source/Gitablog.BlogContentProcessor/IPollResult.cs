using System;
using System.Collections.Generic;

namespace Gitablog.BlogContentProcessor
{
    public interface IPollResult
    {
        bool IsNewCommit { get; set; }
        DateTimeOffset? PushedTimestamp { get; set; }
        string Sha { get; set; }
        List<string> MarkdownFiles { get; set; }
        bool Success { get; }
    }
}