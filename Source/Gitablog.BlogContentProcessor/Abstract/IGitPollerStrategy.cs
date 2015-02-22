using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IGitPollerStrategy
    {
        Task<IPollResult> Poll();
    }
}