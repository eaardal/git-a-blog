using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MarkdownSharp;

namespace Gittablog.GitIntegration
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
