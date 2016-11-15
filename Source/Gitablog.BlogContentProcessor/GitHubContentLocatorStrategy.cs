using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.Infrastructure;
using Octokit;
using Octokit.Internal;

namespace Gitablog.BlogContentProcessor
{
    public class GitHubContentLocatorStrategy : IGitHubContentLocatorStrategy
    {
        private readonly string _apiKey;
        private const string ProductIdentifier = "gitablog";
        private readonly IGitHubRepository _repository;
        private readonly IIoC _ioc;

        public GitHubContentLocatorStrategy(IGitHubRepository repository, IIoC ioc)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (ioc == null) throw new ArgumentNullException("ioc");
            _repository = repository;
            _ioc = ioc;
            _apiKey = Environment.GetEnvironmentVariable("Gitablog.ApiKey");
        }

        public async Task<IGitPollResult> LocateContent()
        {
            var github = new GitHubClient(new ProductHeaderValue(ProductIdentifier), new InMemoryCredentialStore(new Credentials(_apiKey)));
            var repository = await github.Repository.Get(_repository.Owner, _repository.Name);
            var pushedTimestamp = repository.PushedAt;

            var pollResult = _ioc.Resolve<IGitPollResult>();

            if (!pushedTimestamp.HasValue) 
                return pollResult;

            SetIsNewCommit(pollResult);

            SetPushedTimestamp(pollResult, pushedTimestamp);

            UpdateLastPushTimestamp(pushedTimestamp);

            var content = await GetAllFiles(github).ContinueWith(task => task.Result.ToList());
            
            if (!content.Any())
                return pollResult;

            pollResult.MarkdownFiles = content.Where(IsMarkdownFile).Select(file => new RemoteMarkdownFile { Url = file.DownloadUrl.ToString() }).ToList();

            return pollResult;
        }

        private async Task<IEnumerable<RepositoryContent>> GetAllFiles(GitHubClient gitHub)
        {
            return await gitHub.Repository.Content.GetContents(_repository.Owner, _repository.Name, "/");
        }

        private static bool IsMarkdownFile(RepositoryContent content)
        {
            return content.Name.EndsWith(".md");
        }

        private static bool HasFilesToProcess(IEnumerable<GitHubCommitFile> files)
        {
            return files != null && files.Any();
        }

        private async Task<IEnumerable<GitHubCommit>> GetAllCommits(GitHubClient gitHub)
        {
            return await gitHub.Repository.Commits.GetAll(_repository.Owner, _repository.Name);
        } 

        private static async Task<GitHubCommit> GetLastCommit(string owner, string name, GitHubClient github)
        {
            var commits = await github.Repository.Commits.GetAll(owner, name);
            var lastCommit = await github.Repository.Commits.Get(owner, name, commits.First().Sha);
            return lastCommit;
        }

        private void UpdateLastPushTimestamp(DateTimeOffset? pushedTimestamp)
        {
            if (pushedTimestamp.HasValue)
                _repository.LastPushedTicks = pushedTimestamp.Value.Ticks;
        }

        private static void SetPushedTimestamp(IGitPollResult rawContent, DateTimeOffset? pushedTimestamp)
        {
            if (pushedTimestamp.HasValue)
                rawContent.PushedTimestamp = pushedTimestamp.Value;
        }

        private void SetIsNewCommit(IGitPollResult rawContent)
        {
            if (_repository.LastPushedTicks != 0)
                rawContent.IsNewCommit = true;
        }

        private bool IsNewPush(DateTimeOffset? lastUpdate)
        {
            if (lastUpdate.HasValue)
                return _repository.LastPushedTicks == 0 || _repository.LastPushedTicks != lastUpdate.Value.Ticks;
            return true;
        }
    }
}
