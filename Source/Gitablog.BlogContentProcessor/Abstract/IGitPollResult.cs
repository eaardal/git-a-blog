using System;
using System.Collections.Generic;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IGitPollResult
    {
        bool IsNewCommit { get; set; }
        DateTimeOffset? PushedTimestamp { get; set; }
        string Identifier { get; set; }
        IEnumerable<RemoteMarkdownFile> MarkdownFiles { get; set; }
        bool Success { get; }
    }
}