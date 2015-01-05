using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibGit2Sharp;
using Octokit;

namespace Gittablog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {



            return View();
        }
    }
}