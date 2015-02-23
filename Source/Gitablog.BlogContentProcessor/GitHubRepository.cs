using Gitablog.BlogContentProcessor.Abstract;

namespace Gitablog.BlogContentProcessor
{
    public class GitHubRepository : IGitHubRepository
    {
        public string Owner { get { return "eaardal"; } }
        public string Name { get { return "mdtest"; }}
        public long LastPushedTicks { get; set; }
    }
}