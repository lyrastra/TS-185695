using Microsoft.AspNetCore.Http;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class HttpRequestExtensions
{
    internal static string GetUri(this HttpRequest httpRequest)
    {
        return $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}{httpRequest.Path}";
    }

    internal static string GetAuditSpanName(this HttpRequest httpRequest)
    {
        var absPath = $"{httpRequest.PathBase}{httpRequest.Path}".ToLowerInvariant();
            
        return $"{httpRequest.Method} .{absPath}";
    }
}