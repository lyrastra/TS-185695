using System.Net.Http;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Extensions;

internal static class HttpFileModelExtensions
{
    internal static HttpContent CreateMultipartFormDataContent(this HttpFileModel file,
        HttpContent data = null)
    {
        var content = new MultipartFormDataContent();

        var streamContent = new StreamContent(file.Stream);
        streamContent.Headers.Add("Content-Type", file.ContentType);

        content.Add(streamContent, "file", file.FileName);

        if (data != null)
        {
            content.Add(data, "data");
        }

        return content;
    }
}
