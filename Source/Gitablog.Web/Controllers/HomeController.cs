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

        public HomeController(ContentState state)
        {
            if (state == null) throw new ArgumentNullException("state");
            _state = state;
        }

        public async Task<ActionResult> Index(bool forceReload = false)
        {
            var layout = (_state.HasState && !forceReload) ? _state.State : await _state.RequestStateUpdate();

            var pages = ConvertToPages(layout);

            var blog = new Blog {Pages = pages};
            ViewBag.Pages = blog.Pages;
            
            return View(blog);
        }

        private Page ConvertToPage(string pageName, IEnumerable<PostDto> blogEntries)
        {
            return new Page
            {
                Name = pageName,
                BlogPosts = blogEntries.Select(entry => new Post { RawHtml = entry.RawHtml })
            };
        }

        private IEnumerable<Page> ConvertToPages(IEnumerable<KeyValuePair<string, IEnumerable<PostDto>>> layout)
        {
            return layout.Select(grp => ConvertToPage(grp.Key, grp.Value));
        }

        public async Task<ActionResult> Page(string pageName, bool forceReload = false)
        {
            var layout = (_state.HasState && !forceReload) ? _state.State : await _state.RequestStateUpdate();

            if (!string.IsNullOrEmpty(pageName) && layout.ContainsKey(pageName))
            {
                var blogEntries = layout[pageName];

                ViewBag.Pages = ConvertToPages(layout);

                return PartialView("Page", ConvertToPage(pageName, blogEntries));
            }
            return RedirectToAction("Index");
        }
    }
}