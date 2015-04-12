using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IContentRetriever
    {
        Task<IEnumerable<RawMarkdownContent>> Retrieve(IEnumerable<IGitPollResult> rawContents);
    }
}