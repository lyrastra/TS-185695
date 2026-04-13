using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.Xss.WebApi.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.CommonV2.Xss.WebApi;

[InjectAsTransient(typeof(QueryXssValidationHandler))]
public class QueryXssValidationHandler: DelegatingHandler
{
    private XssValidationMode mode = XssValidationMode.RejectSuspiciousRequests;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var queryArgs = request.RequestUri.ParseQueryString();
            foreach (var argument in queryArgs)
            {
                XssValidator.Validate(argument);
            }
        }
        catch (XssValidationException e) when (mode == XssValidationMode.RejectSuspiciousRequests)
        {
            return Task.FromResult(request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
        }

        return base.SendAsync(request, cancellationToken);
    }

    public QueryXssValidationHandler SetMode(XssValidationMode value)
    {
        this.mode = value;

        return this;
    }
}