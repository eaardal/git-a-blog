using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.Infrastructure;
using Octokit;

namespace Gitablog.BlogContentProcessor
{
    class GitHubContentLocatorStrategy : IGitHubContentLocatorStrategy
    {
        private const string ProductIdentifier = "Gittablog";
        private readonly GitHubRepository _repository;
        private readonly IIoC _ioc;

        public GitHubContentLocatorStrategy(GitHubRepository repository, IIoC ioc)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (ioc == null) throw new ArgumentNullException("ioc");
            _repository = repository;
            _ioc = ioc;
        }

        public async Task<IRawContent> LocateContent()
        {
            var github = new GitHubClient(new ProductHeaderValue(ProductIdentifier));
            var repository = await github.Repository.Get(_repository.Owner, _repository.Name);
            var pushedTimestamp = repository.PushedAt;

            var rawContent = _ioc.Resolve<IRawContent>();

            if (!pushedTimestamp.HasValue || !IsNewPush(pushedTimestamp)) return rawContent;

            SetIsNewCommit(rawContent);

            SetPushedTimestamp(rawContent, pushedTimestamp);

            UpdateLastPushTimestamp(pushedTimestamp);

            var lastCommit = await GetLastCommit(_repository.Owner, _repository.Name, github);
            rawContent.Identifier = lastCommit.Sha;

            var files = lastCommit.Files;

            if (!HasFilesToProcess(files)) return rawContent;

            rawContent.MarkdownFiles = files.Where(IsMarkdownFile).Select(file => new RemoteMarkdownFile { Url = file.RawUrl }).ToList();

            return rawContent;
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

        private static void SetPushedTimestamp(IRawContent rawContent, DateTimeOffset? pushedTimestamp)
        {
            if (pushedTimestamp.HasValue)
                rawContent.PushedTimestamp = pushedTimestamp.Value;
        }

        private void SetIsNewCommit(IRawContent rawContent)
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
