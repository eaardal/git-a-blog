using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Gittablog.GitIntegration;
using MarkdownSharp;
using Octokit;

namespace Gittablog.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            new App().Demo().Wait();


            Console.ReadLine();
        }

        class App
        {
            private const string Owner = "eaardal";
            private const string Name = "mdtest";

            public async Task Demo()
            {
                var githubPoller = new GitHubPoller();
                var pollResult = await githubPoller.PollRepository(Owner, Name, 0);

                Console.WriteLine("Success? " + pollResult.Success);
                Console.WriteLine("IsNewCommit? " +pollResult.IsNewCommit);
                Console.WriteLine("PushedTimestamp " + pollResult.PushedTimestamp);
                Console.WriteLine("Sha " +pollResult.Sha);
                pollResult.MarkdownFiles.ForEach(Console.WriteLine);
            }
        }
    }
}
