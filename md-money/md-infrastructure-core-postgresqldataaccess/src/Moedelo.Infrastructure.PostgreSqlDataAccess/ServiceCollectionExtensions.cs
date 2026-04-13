using System;
using Microsoft.Extensions.DependencyInjection;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigurePostgreSqlDbExecutor(this IServiceCollection services,
        Action<MoedeloPostgresqlSqlExecutorOptions> configureOptions)
    {
        services.Configure(configureOptions);

        return services;
    }
}
