using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Logging.Configuration
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureMoedeloCommonLogger(this IWebHostBuilder hostBuilder, string appName, Action<ILoggingBuilder> configureExtraLogFields = null)
        {
            return hostBuilder.ConfigureLogging(builder =>
            {
                builder
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddFilter("Microsoft", LogLevel.Error)
                    .AddFile(options => options.FileName = appName);
                configureExtraLogFields?.Invoke(builder);
            });
        }
    }
}