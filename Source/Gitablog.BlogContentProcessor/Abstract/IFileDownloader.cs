using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IFileDownloader
    {
        Task<string> Download(string url);
    }
}