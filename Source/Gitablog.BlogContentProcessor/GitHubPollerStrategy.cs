using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.Infrastructure;
using Octokit;

namespace Gitablog.BlogContentProcessor
{
    class GitHubPollerStrategy : IGitPollerStrategy
    {
        private const string ProductIdentifier = "Gittablog";
        private readonly GitHubRepository _repository;
        private readonly IIoC _ioc;

        public GitHubPollerStrategy(GitHubRepository repository, IIoC ioc)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (ioc == null) throw new ArgumentNullException("ioc");
            _repository = repository;
            _ioc = ioc;
        }

        public async Task<IPollResult> Poll()
        {
            var github = new GitHubClient(new ProductHeaderValue(ProductIdentifier));
            var repository = await github.Repository.Get(_repository.Owner, _repository.Name);
            var pushedTimestamp = repository.PushedAt;

            var pollResult = _ioc.Resolve<IPollResult>();

            if (!pushedTimestamp.HasValue || !IsNewPush(pushedTimestamp)) return pollResult;

            SetIsNewCommit(pollResult);

            SetPushedTimestamp(pollResult, pushedTimestamp);

            UpdateLastPushTimestamp(pushedTimestamp);

            var lastCommit = await GetLastCommit(_repository.Owner, _repository.Name, github);
            pollResult.Sha = lastCommit.Sha;

            var files = lastCommit.Files;

            if (!HasFilesToProcess(files)) return pollResult;

            pollResult.MarkdownFiles = files.Where(IsMarkdownFile).Select(file => new MarkdownWebFile { Url = file.RawUrl }).ToList();

            return pollResult;
        }

        private static bool IsMarkdownFile(GitHubCommitFile file)
        {
            return file.Filename.EndsWith(".md");
        }

        private static bool HasFilesToProcess(IEnumerable<GitHubCommitFile> files)
        {
            return files != null && files.Any();
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

        private static void SetPushedTimestamp(IPollResult pollResult, DateTimeOffset? pushedTimestamp)
        {
            if (pushedTimestamp.HasValue)
                pollResult.PushedTimestamp = pushedTimestamp.Value;
        }

        private void SetIsNewCommit(IPollResult pollResult)
        {
            if (_repository.LastPushedTicks != 0)
                pollResult.IsNewCommit = true;
        }

        private bool IsNewPush(DateTimeOffset? lastUpdate)
        {
            if (lastUpdate.HasValue)
                return _repository.LastPushedTicks == 0 || _repository.LastPushedTicks != lastUpdate.Value.Ticks;
            return true;
        }
    }
}
