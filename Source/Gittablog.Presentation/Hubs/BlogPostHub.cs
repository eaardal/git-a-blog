using Gittablog.Presentation.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Gittablog.Presentation.Hubs
{
    [HubName("blogPostHub")]
    public class BlogPostHub : Hub
    {
        
    }
}