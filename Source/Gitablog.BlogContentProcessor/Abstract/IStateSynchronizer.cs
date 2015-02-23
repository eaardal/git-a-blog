using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IStateSynchronizer
    {
        Task<IDictionary<string, IEnumerable<BlogEntry>>> Synchronize();
    }
}