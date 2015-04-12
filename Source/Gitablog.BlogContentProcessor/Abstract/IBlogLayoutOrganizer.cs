using System.Collections.Generic;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IBlogLayoutOrganizer
    {
        IDictionary<string, IEnumerable<PostDto>> Organize(IEnumerable<PostDto> blogEntries);
    }
}