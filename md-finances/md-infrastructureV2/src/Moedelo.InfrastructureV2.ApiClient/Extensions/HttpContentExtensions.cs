using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions
{
    internal static class HttpContentExtensions
    {
        internal static HttpContent CreateFormUrlEncodedContent(
            this IReadOnlyList<KeyValuePair<string, string>> data)
        {
            try
            {
                return new FormUrlEncodedContent(data);
            }
            catch (UriFormatException)
            {
                var content = new MultipartFormDataContent();
                foreach (var keyValuePair in data)
                {
                    content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                }

                return content;
            }
        }
    }
}
