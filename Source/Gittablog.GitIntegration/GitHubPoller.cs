using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MarkdownSharp;
using Octokit;

namespace Gittablog.GitIntegration
{
    public class GitHubPoller
    {
        private const string ProductIdentifier = "Gittablog";
        private long _lastPushedTicks;

        public async Task<IPollResult> PollRepository(string owner, string name, int intervalMilliseconds)
        {
            var github = new GitHubClient(new ProductHeaderValue(ProductIdentifier));
            var repository = await github.Repository.Get(owner, name);
            var lastUpdate = repository.PushedAt;

            var pollResult = new PollResult();

            if (lastUpdate.HasValue)
            {
                if (_lastPushedTicks == 0 || _lastPushedTicks != lastUpdate.Value.Ticks)
                {
                    if (_lastPushedTicks != 0)
                        pollResult.IsNewCommit = true;

                    pollResult.PushedTimestamp = lastUpdate.Value;
                    
                    _lastPushedTicks = lastUpdate.Value.Ticks;

                    var commits = await github.Repository.Commits.GetAll(owner, name);
                    var lastCommit = await github.Repository.Commits.Get(owner, name, commits.First().Sha);
                    pollResult.Sha = lastCommit.Sha;
                    
                    var files = lastCommit.Files;

                    if (files != null && files.Any())
                    {
                        foreach (var file in files)
                        {
                            if (file.Filename.EndsWith(".md"))
                            {
                                var downloader = new FileDownloader();
                                var fileContent = await downloader.Download(file.RawUrl);

                                var md = new MarkdownUtil();
                                var markdown = md.Convert(fileContent);
                                pollResult.MarkdownFiles.Add(markdown);
                            }
                        }
                    }
                }
            }

            return pollResult;
        }
    }
}
