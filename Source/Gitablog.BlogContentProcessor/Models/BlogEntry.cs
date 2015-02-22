using System;
using System.Collections.Generic;

namespace Gitablog.BlogContentProcessor.Models
{
    public class BlogEntry
    {
        public DateTime LastUpdated { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Category { get; set; }
        public string RawHtml { get; set; }
    }
}
