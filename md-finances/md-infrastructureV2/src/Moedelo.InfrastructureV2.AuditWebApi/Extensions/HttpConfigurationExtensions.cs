#nullable enable
using System.Web.Http;
using Moedelo.InfrastructureV2.AuditWebApi.Handlers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.AuditWebApi.Extensions;

public static class HttpConfigurationExtensions
{
    public static HttpConfiguration SetupMoedeloAuditTrail(this HttpConfiguration httpConfiguration,
        bool saveClientIpAddress, bool saveUserAgent = false)
    {
        var auditTrailHeaderInjectionHandler = httpConfiguration.DependencyResolver
            .EnsureGetService<AuditTrailHeaderInjectionHandler>(); 
        var webApiAuditFilter = httpConfiguration.DependencyResolver
            .EnsureGetService<AuditTrailWebApiFilter>()
            .TagClientIpAddress(saveClientIpAddress)
            .TagClientUserAgent(saveUserAgent);
        var logger = httpConfiguration.DependencyResolver
            .EnsureGetService<ILogger>();
        var loggerExtender = httpConfiguration.DependencyResolver
            .EnsureGetService<IAuditTrailHttpRequestLogEventExtender>();

        // добавляем расширение логирования, которое будет добавлять в лог информацию об audit trail контексте
        logger.AddLogEventExtender(loggerExtender);
        // добавляем фильтр, который будет создавать контекст audit trail при обработке web api запросов
        httpConfiguration.Filters.Add(webApiAuditFilter);
        // добавляем в начало цепочки обработчик, который будет добавлять заголовки audit trail в ответы
        httpConfiguration.MessageHandlers.Insert(0, auditTrailHeaderInjectionHandler);

        return httpConfiguration;
    }
}
