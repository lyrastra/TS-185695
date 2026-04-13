using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Moedelo.Infrastructure.DependencyInjection.Warmup;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавить проверку создаваемости типов, зарегистрированных в DI  
    /// </summary>
    public static IServiceCollection AddWarmup(
        this IServiceCollection services,
        Action<ApplicationWarmupOptions> setupWarmUpOptions = null)
    {
        services.TryAddSingleton(services);

        var options = new ApplicationWarmupOptions();
        setupWarmUpOptions?.Invoke(options);
        services.AddSingleton(options);

        return services.AddHostedService<WarmupHostedService>();
    }
}