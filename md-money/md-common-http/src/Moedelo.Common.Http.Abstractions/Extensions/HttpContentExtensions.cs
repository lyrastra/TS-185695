using System.Net.Http;
using System.Text;
using Moedelo.Common.Http.Abstractions.Internals;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Http.Abstractions.Extensions
{
    internal static class HttpContentExtensions
    {
        internal static HttpJsonStringContent ToUtf8JsonContent<TRequest>(this TRequest data) where TRequest : class
        {
            var content = data.ToJsonString();
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            return new HttpJsonStringContent(httpContent, content);
        }
    }
}