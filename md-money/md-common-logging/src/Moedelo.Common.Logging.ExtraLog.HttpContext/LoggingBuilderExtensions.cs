using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.HttpContext
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddHttpContextExtraLogFields(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<IExtraLogFieldsProvider, HttpContextExtraLogFieldsProvider>();

            return builder;
        }
    }
}