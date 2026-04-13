using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Infrastructure.Consul.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<TResponseContent> DeserializeJsonContentAsync<TResponseContent>(
        this HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
    {
        responseMessage.EnsureSuccessStatusCode();

        using var responseStream = await responseMessage.Content
            .ReadAsStreamAsync()
            .ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();

        return responseStream.FromJsonStream<TResponseContent>();
    }

    public static void EnsureSuccessStatusCodeEx(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        throw new Exception(string.Format("Http request by method {0} to \"{1}\" returns bad status code {2} ({3})",
            response.RequestMessage.Method.Method,
            response.RequestMessage.RequestUri,
            (int)response.StatusCode,
            response.ReasonPhrase)
        );
    }
}
