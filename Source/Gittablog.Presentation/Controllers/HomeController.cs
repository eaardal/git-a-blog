using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gittablog.GitIntegration;
using Gittablog.Presentation.Hubs;
using Gittablog.Presentation.Models;
using LibGit2Sharp;
using Microsoft.AspNet.SignalR;
using Octokit;

namespace Gittablog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var viewModel = new HomeViewModel();
            var github = new GitHubPoller();
            var hub = new BlogPostHubWrapper();
            var poller = new TempTimerPoller();

            poller.Start(async () =>
            {
                var pollResult = await github.PollRepository("eaardal", "mdtest");

                var posts = pollResult.MarkdownFiles.Select(file => new BlogPost { HtmlContent = file });

                foreach (var post in posts)
                {
                    hub.BroadcastBlogPost(post);
                }

            }, 10000);

      
            return View();
        }
    }
}