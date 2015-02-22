using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Gitablog.BlogContentProcessor
{
    public class FileDownloader
    {
        public async Task<string> Download(string url)
        {
            var webClient = new WebClient();
            using (var stream = new MemoryStream(await webClient.DownloadDataTaskAsync(url)))
            {
                stream.Position = 0;
                var sr = new StreamReader(stream);
                return sr.ReadToEnd();
            }
        }
    }
}
