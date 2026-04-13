using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    public class FileActionResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpContent content;
        private readonly string fileName;

        public string ContentType { get; set; } = "application/octet-stream";
        public string DispositionType { get; set; } = "attachment";

        public FileActionResult(HttpRequestMessage request, byte[] content, string fileName)
        {
            this.request = request;
            this.content = new ByteArrayContent(content);
            this.fileName = fileName;
        }

        public FileActionResult(HttpRequestMessage request, Stream content, string fileName)
        {
            this.request = request;
            this.content = new StreamContent(content);
            this.fileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = content;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(DispositionType)
            {
                FileNameStar = fileName
            };
            return Task.FromResult(response);
        }
    }
}
