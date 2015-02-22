using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IGitContentLocatorStrategy
    {
        Task<IRawContent> LocateContent();
    }
}