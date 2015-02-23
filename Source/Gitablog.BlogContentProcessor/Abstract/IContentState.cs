using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IContentState
    {
        event ContentState.StateUpdatedEventHandler StateUpdated;
        IDictionary<string, IEnumerable<BlogEntry>> State { get; set; }
        bool HasState { get; }
        Task<IDictionary<string, IEnumerable<BlogEntry>>> RequestStateUpdate();
    }
}