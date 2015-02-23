using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gitablog.BlogContentProcessor;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.Web.ViewModels;

namespace Gitablog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContentState _state;
        private readonly BlogContentEngine _engine;

        public HomeController(ContentState state)
        {
            if (state == null) throw new ArgumentNullException("state");
            _state = state;
        }

        public async Task<ActionResult> Index()
        {
            var layout = _state.State ?? await _state.RequestStateUpdate();

            var pages = ConvertToPages(layout);

            return View(new Blog{ Pages = pages });
        }

        private Page ConvertToPage(string pageName, IEnumerable<BlogEntry> blogEntries)
        {
            var page = new Page
            {
                Name = pageName
            };

            foreach (var entry in blogEntries)
            {
                page.BlogPosts.Add(new BlogPost { RawHtml = entry.RawHtml });
            }

            return page;
        }

        private IEnumerable<Page> ConvertToPages(IEnumerable<KeyValuePair<string, IEnumerable<BlogEntry>>> layout)
        {
            var pages = new List<Page>();

            foreach (var grp in layout)
            {
                pages.Add(ConvertToPage(grp.Key, grp.Value));
            }
            return pages;
        }

        public async Task<ActionResult> Category(string category)
        {
            var layout = _state.State ?? await _state.RequestStateUpdate();
            
            if (layout.ContainsKey(category))
            {
                var blogEntries = layout[category];

                return PartialView("Page", ConvertToPage(category, blogEntries));
            }
            return RedirectToAction("Index");
        }
    }
}