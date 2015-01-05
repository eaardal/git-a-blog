using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gittablog.GitIntegration;
using Gittablog.Presentation.Models;
using LibGit2Sharp;
using Octokit;

namespace Gittablog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var github = new GitHubPoller();

            var pollResult = await github.PollRepository("eaardal", "mdtest", 0);

            var viewModel = new HomeViewModel {PollResult = pollResult};
            
            return View(viewModel);
        }
    }
}