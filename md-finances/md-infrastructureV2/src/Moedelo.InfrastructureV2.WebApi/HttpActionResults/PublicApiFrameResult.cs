using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    public class PublicApiFrameResult: IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpStatusCode statusCode;
        private readonly IReadOnlyCollection<object> data;
        private readonly int offset;
        private readonly int limit;
        private readonly int totalCount;
        
        public PublicApiFrameResult(HttpRequestMessage request, HttpStatusCode statusCode, IReadOnlyCollection<object> data, int offset, int limit, int totalCount)
        {
            this.request = request;
            this.statusCode = statusCode;
            this.data = data;
            this.offset = offset;
            this.limit = limit;
            this.totalCount = totalCount;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var value = new
            {
                data,
                offset,
                limit,
                totalCount
            };
            var result = request.CreateResponse(statusCode, value);
            return Task.FromResult(result);
        }
    }
}