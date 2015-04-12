using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IBlogContentEngine
    {
        Task<IDictionary<string, IEnumerable<PostDto>>> GetBlogContent();
    }
}