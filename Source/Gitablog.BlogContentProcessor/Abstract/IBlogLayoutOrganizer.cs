using System.Collections.Generic;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IBlogLayoutOrganizer
    {
        IDictionary<string, IEnumerable<BlogEntry>> Organize(IEnumerable<BlogEntry> blogEntries);
    }
}