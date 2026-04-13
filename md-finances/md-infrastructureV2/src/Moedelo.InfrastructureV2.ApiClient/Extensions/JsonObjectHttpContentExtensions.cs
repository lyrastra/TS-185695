using System.Net.Http;
using System.Net.Http.Formatting;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions;

internal static class JsonObjectHttpContentExtensions
{
    private static readonly JsonMediaTypeFormatter jsonMediaTypeFormatter = new ();
    
    internal static HttpContent ToJsonObjectHttpContent<TRequest>(this TRequest data)
    {
        return new ObjectContent<TRequest>(data, jsonMediaTypeFormatter);
    }
}
