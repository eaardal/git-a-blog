using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
            private Timer _timer;
            private long _lastPushedTicks;
            private const string Owner = "eaardal";
            private const string Name = "mdtest";

            public async Task Demo()
            {
                var github = new GitHubClient(new ProductHeaderValue("Gittablog"));

                _timer = new Timer(500);
                _timer.Elapsed += async (sender, args) =>
                {
                    var repository = await github.Repository.Get(Owner, Name);
                    var lastUpdate = repository.PushedAt;
                    if (lastUpdate.HasValue)
                    {
                        if (_lastPushedTicks == 0 || _lastPushedTicks != lastUpdate.Value.Ticks)
                        {
                            if (_lastPushedTicks != 0)
                                Console.WriteLine("New commit detected");

                            _lastPushedTicks = lastUpdate.Value.Ticks;

                            var commits = await github.Repository.Commits.GetAll(Owner, Name);

                            var lastCommit = await github.Repository.Commits.Get(Owner, Name, commits.First().Sha);

                            var files = lastCommit.Files;

                            foreach (var file in files)
                            {
                                Console.WriteLine(file);

                                if (file.Filename.EndsWith(".md"))
                                {
                                    var webClient = new WebClient();
                                    using (var stream = new MemoryStream(await webClient.DownloadDataTaskAsync(file.RawUrl)))
                                    {
                                        stream.Position = 0;
                                        var sr = new StreamReader(stream);
                                        var myStr = sr.ReadToEnd();

                                        var fileContent = myStr;
                                        var mark = new Markdown();
                                        var markdown = mark.Transform(fileContent);
                                    }
                                }
                            }

                        }
                    }

                    Console.WriteLine(repository.FullName + " last update: " + lastUpdate);
                };
                _timer.Start();

                Console.WriteLine("Started timer");
            }
        }
    }
}
