using System;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;

public static class UriExtensions
{
    public static string GetAuditSpanName(this Uri uri, string httpMethod)
    {
        return $"{httpMethod} .{uri.AbsolutePath?.ToLowerInvariant()}";
    }
}