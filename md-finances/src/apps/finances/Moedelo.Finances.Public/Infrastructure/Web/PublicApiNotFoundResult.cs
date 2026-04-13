using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.Finances.Public.Infrastructure.Web
{
    public class PublicApiNotFoundResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpStatusCode statusCode;
        private readonly string message;

        public PublicApiNotFoundResult(HttpRequestMessage request, HttpStatusCode statusCode, string message)
        {
            this.request = request;
            this.statusCode = statusCode;
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = request.CreateResponse(statusCode, new { NotFound = new { message } });
            return Task.FromResult(result);
        }
    }
}