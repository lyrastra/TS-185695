using Microsoft.Extensions.DependencyInjection;

namespace Moedelo.Common.AspNet.HostedServices.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет реализацию фонового сервиса типа <see cref="MoedeloPeriodicHostedService"/>
    /// </summary>
    public static IServiceCollection AddPeriodicHostedService<T>(this IServiceCollection services)
        where T: MoedeloPeriodicHostedService
    {
        return services.AddHostedService<T>();
    }
}
