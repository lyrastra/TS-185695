using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.ExtraData
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddExtraDataContextExtraLogFields(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<IExtraLogFieldsProvider, ExtraDataContextExtraLogFieldsProvider>();

            return builder;
        }
    }
}