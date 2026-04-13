using System;
using Microsoft.Extensions.DependencyInjection;
using Moedelo.Infrastructure.SqlDataAccess.Options;

namespace Moedelo.Infrastructure.SqlDataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMsSqlDbExecutor(this IServiceCollection services,
        Action<MsSqlDbExecutorOptions> configureOptions)
    {
        services.Configure(configureOptions);

        return services;
    }
}
