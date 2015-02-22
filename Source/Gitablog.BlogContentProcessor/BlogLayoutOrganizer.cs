using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor
{
    public class BlogLayoutOrganizer
    {
        public async Task<IDictionary<string, IEnumerable<BlogEntry>>> Organize(IEnumerable<BlogEntry> blogEntries)
        {
            var result = new Dictionary<string, IEnumerable<BlogEntry>>();

            return result;
        } 
    }
}
