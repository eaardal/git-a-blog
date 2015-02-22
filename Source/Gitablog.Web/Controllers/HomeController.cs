using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gitablog.BlogContentProcessor;
using Gitablog.Web.Models;

namespace Gitablog.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var viewModel = new HomeViewModel();
            var github = new GitHubPoller();
            
            var pollResult = await github.PollRepository("eaardal", "mdtest");

            viewModel.PollResult = pollResult;

            return View(viewModel);
        }
    }
}