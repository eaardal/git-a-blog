using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gitablog.Web.ViewModels
{
    public class Page
    {
        public string Name { get; set; }
        public IEnumerable<Post> BlogPosts { get; set; }

        public Page()
        {
            BlogPosts = new List<Post>();
        }
    }
}