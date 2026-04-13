using System.Net.Http;
using System.Net.Http.Headers;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions;

internal static class HttpFileStreamExtensions
{
    internal static MultipartFormDataContent ToMultipartFormDataContent<TRequest>(this HttpFileStream file,
        TRequest request)
        where TRequest : class
    {
        var formDataContent = file.ToMultipartFormDataContent();
        formDataContent.Add(request.ToJsonObjectHttpContent(), "data");

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
