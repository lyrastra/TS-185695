using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    /// <inheritdoc />
    /// <summary>
    /// Код HttpStatusCode.NotFound с указанным сообщением в теле
    /// </summary>
    public class NotFoundExResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly string message;

        public NotFoundExResult(HttpRequestMessage request, string message)
        {
            this.request = request;
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError(message)));
        }
    }
}