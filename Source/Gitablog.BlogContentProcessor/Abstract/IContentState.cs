using System.Collections.Generic;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Models;

namespace Gitablog.BlogContentProcessor.Abstract
{
    public interface IContentState
    {
        event ContentState.StateUpdatedEventHandler StateUpdated;
        IDictionary<string, IEnumerable<PostDto>> State { get; set; }
        bool HasState { get; }
        Task<IDictionary<string, IEnumerable<PostDto>>> RequestStateUpdate();
    }
}