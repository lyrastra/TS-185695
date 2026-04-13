using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    public class PublicApiErrorResult: IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpStatusCode statusCode;
        private readonly string[] messages;
        
        public PublicApiErrorResult(HttpRequestMessage request, HttpStatusCode statusCode, params string[] messages)
        {
            this.request = request;
            this.statusCode = statusCode;
            this.messages = messages;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = request.CreateResponse(statusCode, new { errors = new { messages } });
            return Task.FromResult(result);
        }
    }
}