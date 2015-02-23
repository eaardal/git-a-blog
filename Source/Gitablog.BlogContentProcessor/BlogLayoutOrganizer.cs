using System.Collections.Generic;
using System.Linq;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class BlogLayoutOrganizer : IBlogLayoutOrganizer
    {
        public IDictionary<string, IEnumerable<BlogEntry>> Organize(IEnumerable<BlogEntry> blogEntries)
        {
            return blogEntries.GroupBy(entry => entry.Category)
                .ToDictionary(group => group.Key, group => group.Select(entry => entry));
        } 
    }
}
