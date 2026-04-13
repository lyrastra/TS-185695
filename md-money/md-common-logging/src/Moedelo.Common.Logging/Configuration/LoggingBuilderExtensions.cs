using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Logging.ExtraLog.Abstractions;
using Moedelo.Common.Logging.LoggerProviders;
using Moedelo.Common.Logging.Options;

namespace Moedelo.Common.Logging.Configuration
{
    internal static class LoggingBuilderExtensions
    {
        internal static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            builder.Services.Configure(configure);

            return builder;
        }

        internal static ILoggingBuilder AddBoxField(this ILoggingBuilder builder)
        {
            var hostGroupValue = Environment.GetEnvironmentVariable("MD_LOGGING_BOX_FIELD");

            if (!string.IsNullOrEmpty(hostGroupValue))
            {
                builder.Services.AddSingleton<IExtraLogFieldsProvider>(
                    _ => new ExtraFieldLoggerProvider("BOX", hostGroupValue));
            }

            return builder;
        }
    }
}