using System.Collections.Generic;
using System.Net.Http;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions
{
    internal static class HttpRequestMessageExtensions
    {
        internal static void AddHeaders(
            this HttpRequestMessage requestMessage,
            IReadOnlyCollection<KeyValuePair<string, string>> headers)
        {
            if (headers == null)
            {
                return;
            }

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
