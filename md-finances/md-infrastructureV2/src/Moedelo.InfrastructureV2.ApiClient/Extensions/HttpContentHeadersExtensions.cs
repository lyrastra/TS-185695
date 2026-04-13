using System.Net.Http.Headers;

namespace Moedelo.InfrastructureV2.ApiClient.Extensions;

internal static class HttpContentHeadersExtensions
{
    internal static string ParseFileName(this HttpContentHeaders httpContentHeaders)
    {
        if (httpContentHeaders.ContentDisposition == null)
        {
            return null;
        }

        // проверяем сначала параметр "filename*" (название файла с учетом кодировки) 
        var fileNameStar = httpContentHeaders.ContentDisposition.FileNameStar;
        if (!string.IsNullOrEmpty(fileNameStar))
        {
            return fileNameStar.Trim('\"');
        }

        return httpContentHeaders.ContentDisposition.FileName?.Trim('\"');
    }
}
