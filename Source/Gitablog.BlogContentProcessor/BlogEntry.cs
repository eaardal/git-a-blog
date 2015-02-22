using System;
using System.Collections.Generic;

namespace Gitablog.BlogContentProcessor
{
    public class BlogEntry
    {
        public DateTime LastUpdated { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string RawHtml { get; set; }
    }
}
