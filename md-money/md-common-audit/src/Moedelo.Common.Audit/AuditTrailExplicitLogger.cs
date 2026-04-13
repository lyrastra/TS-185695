using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Configuration.Interfaces;
using Moedelo.Common.Audit.Logging.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Audit;

[InjectAsSingleton(typeof(IAuditTrailExplicitLogger))]
internal sealed class AuditTrailExplicitLogger : IAuditTrailExplicitLogger
{
    private readonly IAuditConfig config;
    private readonly IAuditSpanLogger spanLogger;

    public AuditTrailExplicitLogger(
        IAuditConfig config,
        IAuditSpanLogger spanLogger)
    {
        this.config = config;
        this.spanLogger = spanLogger;
    }

    public void WriteSpan(IAuditSpanData span)
    {
        if (!config.IsEnabled(span.Type))
        {
            return;
        }

        spanLogger.FireAndForgetLog(span);
    }
}
