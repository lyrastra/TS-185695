using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.Finances.Public.Infrastructure.Web
{
    public class PublicApiDataResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpStatusCode statusCode;
        private readonly object data;

        public PublicApiDataResult(HttpRequestMessage request, HttpStatusCode statusCode, object data)
        {
            this.request = request;
            this.statusCode = statusCode;
            this.data = data;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = request.CreateResponse(statusCode, new { data });
            return Task.FromResult(result);
        }
    }
}