#nullable enable
using System.Net.Http;
using System.Web;

namespace Moedelo.InfrastructureV2.AuditWebApi.Extensions;

internal static class HttpContextExtensions
{
    private const string RequestMessageItemName = "MS_HttpRequestMessage"; 
    
    internal static HttpRequestMessage? GetHttpRequestMessage(this HttpContext httpContext)
        => httpContext.Items.Contains(RequestMessageItemName)
            ? httpContext.Items[RequestMessageItemName] as HttpRequestMessage
            : null;
}
