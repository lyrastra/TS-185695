using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Logging.Configuration
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureMoedeloCommonLogger(this IHostBuilder hostBuilder, string appName, Action<ILoggingBuilder> configureExtraLogFields = null)
        {
            return hostBuilder.ConfigureLogging(builder =>
            {
                builder
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddFilter("Microsoft", LogLevel.Error)
                    .AddFile(options => options.FileName = appName)
                    .AddBoxField();
                configureExtraLogFields?.Invoke(builder);
            });
        }
        
        private static IHostBuilder ConfigureLogging(
            this IHostBuilder hostBuilder,
            Action<ILoggingBuilder> configureLogging)
        {
            return hostBuilder.ConfigureServices(collection => collection.AddLogging(configureLogging));
        }

        private static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder, Action<IServiceCollection> configureServices)
        {
            if (configureServices == null)
            {
                throw new ArgumentNullException(nameof (configureServices));
            }
            
            return hostBuilder.ConfigureServices((_, services) => configureServices(services));
        }
    }
}