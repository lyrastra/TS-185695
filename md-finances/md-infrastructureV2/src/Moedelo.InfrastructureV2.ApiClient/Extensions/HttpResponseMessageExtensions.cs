using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<HttpFileModel> ReadHttpFileModelAsync(this HttpResponseMessage response, CancellationToken cancellationToken)
    {
        using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var stream = new MemoryStream();
        const int bufferSize = 81920;
        await responseStream.CopyToAsync(stream, bufferSize, cancellationToken).ConfigureAwait(false);

        stream.Position = 0;

        var httpContentHeaders = response.Content.Headers;

        return new HttpFileModel
        {
            Stream = stream,
            ContentType = httpContentHeaders.ContentType?.MediaType,
            FileName = httpContentHeaders.ParseFileName()
        };
    }
}
