namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IGitHubRepository
    {
        string Owner { get; }
        string Name { get; }
        long LastPushedTicks { get; set; }
    }
}