using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    /// <inheritdoc />
    /// <summary>
    /// Код HttpStatusCode.Forbidden с указанным сообщением в теле
    /// </summary>
    public class ForbiddenExResult : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly string message;

        public ForbiddenExResult(HttpRequestMessage request, string message)
        {
            this.request = request;
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError(message)));
        }
    }
}