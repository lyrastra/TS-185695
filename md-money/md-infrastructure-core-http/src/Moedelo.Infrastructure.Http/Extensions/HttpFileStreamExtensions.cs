using System.Net.Http;
using System.Net.Http.Headers;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Infrastructure.Http.Extensions;

internal static class HttpFileStreamExtensions
{
    internal static MultipartFormDataContent ToMultipartFormDataContent(this HttpFileStream file,
        HttpContent data)
    {
        var formDataContent = file.ToMultipartFormDataContent();
        formDataContent.Add(data, "data");

        return formDataContent;
    }

    internal static MultipartFormDataContent ToMultipartFormDataContent(this HttpFileStream file)
    {
        var formDataContent = new MultipartFormDataContent();
        var streamContent = file.MapToFormDataStreamContent();
        formDataContent.Add(streamContent, "file", file.FileName);

        return formDataContent;
    }
    
    private static StreamContent MapToFormDataStreamContent(this HttpFileStream file)
    {
        var streamContent = new StreamContent(file.Stream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        // ContentDisposition выставлять вручную не надо

        return streamContent;
    }
}
