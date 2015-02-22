using Gittablog.Presentation.Models;
using Microsoft.AspNet.SignalR;

namespace Gittablog.Presentation.Hubs
{
    public class BlogPostHubWrapper
    {
        private readonly IHubContext _hubContext;
 
        public BlogPostHubWrapper()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<BlogPostHub>();
        }

        public void BroadcastBlogPost(BlogPost post)
        {
            _hubContext.Clients.All.broadcastBlogPost(post);
        }
    }
}