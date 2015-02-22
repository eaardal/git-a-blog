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
            var layout = await _engine.GetBlogContent();

            var pages = new List<Page>();

            foreach (var grp in layout)
            {
                var page = new Page
                {
                    Name = grp.Key
                };

                foreach (var entry in grp.Value)
                {
                    page.BlogPosts.Add(new BlogPost { RawHtml = entry.RawHtml });
                }

                pages.Add(page);
            }

            return View(pages);
        }
    }
}