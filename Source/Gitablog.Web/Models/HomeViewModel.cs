using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gitablog.BlogContentProcessor;
using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.Web.Models
{
    public class HomeViewModel
    {
        public IPollResult PollResult { get; set; }
    }
}