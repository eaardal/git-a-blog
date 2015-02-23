using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IContentLocator
    {
        Task<IEnumerable<IRawContent>> Locate();
    }
}