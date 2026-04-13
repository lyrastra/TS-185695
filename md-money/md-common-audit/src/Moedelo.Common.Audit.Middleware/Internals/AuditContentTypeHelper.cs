namespace Moedelo.Common.Audit.Middleware.Internals;

internal static class AuditContentTypeHelper
{
    private static readonly string[] AuditableContentType =
    [
        "application/json",
        "application/xml"
    ];

    internal static bool CanDumpContentWithContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return false;
        }

        foreach (var marker in AuditableContentType)
        {
            if (contentType.StartsWith(marker))
            {
                return true;
            }
        }

        return false;
    }
}