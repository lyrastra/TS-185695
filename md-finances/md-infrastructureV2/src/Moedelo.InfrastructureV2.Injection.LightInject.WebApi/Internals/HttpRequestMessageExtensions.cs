#nullable enable
using System.Net.Http;
using System.Web;
using Moedelo.InfrastructureV2.Injection.LightInject.Web;

namespace Moedelo.InfrastructureV2.Injection.LightInject.WebApi.Internals;

internal static class HttpRequestMessageExtensions
{
    internal static HttpContextBase? GetHttContext(this HttpRequestMessage requestMessage)
    {
        return requestMessage.Properties.TryGetValue(HttpPropertyCustomKeys.MsHttpContext, out var value)
            ? value as HttpContextWrapper
            : null;
    }
}
