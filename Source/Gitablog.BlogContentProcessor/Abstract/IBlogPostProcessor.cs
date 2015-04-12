using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IBlogPostProcessor
    {
        Task<IEnumerable<PostDto>> Process(IEnumerable<RawMarkdownContent> rawMarkdownContents);
    }
}