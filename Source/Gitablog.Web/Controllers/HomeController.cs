using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gitablog.BlogContentProcessor;
using Gitablog.Web.ViewModels;

namespace Gitablog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContentEngine _engine;

        public HomeController(BlogContentEngine engine)
        {
            if (engine == null) throw new ArgumentNullException("engine");
            _engine = engine;
        }

        public async Task<ActionResult> Index()
        {
            var blogEntries = await _engine.GetBlogContent();

            var posts = blogEntries.Select(entry => new BlogPost {HtmlContent = entry.RawHtml});

            return View(posts);
        }
    }
}