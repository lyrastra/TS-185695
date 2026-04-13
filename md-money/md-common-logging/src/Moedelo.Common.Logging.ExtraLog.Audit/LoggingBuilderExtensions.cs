using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.Audit
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddAuditInfoContextExtraLogFields(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<IExtraLogFieldsProvider, AuditSpanContextExtraLogFieldsProvider>();

            return builder;
        }
    }
}