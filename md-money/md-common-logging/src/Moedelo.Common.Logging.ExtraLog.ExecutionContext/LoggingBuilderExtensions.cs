using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.ExecutionContext
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddExecutionInfoContextExtraLogFields(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<IExtraLogFieldsProvider, ExecutionInfoContextExtraLogFieldsProvider>();

            return builder;
        }
    }
}