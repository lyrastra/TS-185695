using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Logging.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMoedeloCommonLogger(
            this IServiceCollection collection,
            string appName,
            Action<ILoggingBuilder> configureExtraLogFields = null)
        {
            return collection.AddLogging(builder =>
            {
                builder
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddFilter("Microsoft", LogLevel.Error)
                    .AddFile(options => options.FileName = appName)
                    .AddBoxField();;
                configureExtraLogFields?.Invoke(builder);
            });
        }
    }
}