using System;
using System.Collections.Generic;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IPollResult
    {
        bool IsNewCommit { get; set; }
        DateTimeOffset? PushedTimestamp { get; set; }
        string Sha { get; set; }
        IEnumerable<MarkdownWebFile> MarkdownFiles { get; set; }
        bool Success { get; }
    }
}