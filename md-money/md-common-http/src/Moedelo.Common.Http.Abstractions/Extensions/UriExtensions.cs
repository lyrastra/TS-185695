using System;

namespace Moedelo.Common.Http.Abstractions.Extensions
{
    public static class UriExtensions
    {
        public static string GetAuditSpanName(this Uri uri, string httpMethod)
        {
            return $"{httpMethod} .{uri.AbsolutePath?.ToLowerInvariant()}";
        }
    }
}
