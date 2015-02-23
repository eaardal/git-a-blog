using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gitablog.Web.ViewModels
{
    public class Blog
    {
        public IEnumerable<Page> Pages { get; set; }
        public string Name { get; set; }
    }
}