using Microsoft.Owin;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.AuditOwin.Extensions;

public static class AuditSpanBuilderExtensions
{
    public static void AddRequestTags(this IAuditSpanBuilder scope, IOwinRequest request)
    {
        scope.WithTag("Request.Method", request.Method);
        scope.WithTag("Request.OriginalUri", request.Uri.OriginalString.MaskSecureParamsInQueryString());

        // Добавляем IP адрес клиента
        var clientIp = request.GetClientIpAddress();

        if (!string.IsNullOrEmpty(clientIp))
        {
            scope.WithTag("Request.IP", clientIp);
        }

        // Добавляем User-Agent
        var userAgent = request.Headers["User-Agent"];
        if (!string.IsNullOrEmpty(userAgent))
        {
            scope.WithTag("Request.UserAgent", userAgent);
        }
    }
}
