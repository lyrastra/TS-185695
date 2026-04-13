using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Infrastructure.AspNetCore.Mvc.RouteConstraints;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;

/// <summary>
/// Набор расширений для настройки типовых MVC-компонентов МоёДело.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключает стандартное поведение API (обёртка ошибок в <see cref="ApiValidationErrorResult"/>)
    /// и позволяет дополнительно настроить <see cref="ApiBehaviorOptions"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="configureOptions">Дополнительная конфигурация поведения API.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="services"/> не задан.</exception>
    public static void AddMoedeloApiResponseBehavior(this IServiceCollection services,
        Action<ApiBehaviorOptions>? configureOptions = null)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory =
                context => new ApiValidationErrorResult(context.ModelState);
            configureOptions?.Invoke(options);
        });
    }

    /// <summary>
    /// Автоматически регистрирует как <see cref="IHostedService"/> все типы в указанной сборке,
    /// удовлетворяющие условиям:
    /// <list type="number">
    /// <item><description>класс не является абстрактным;</description></item>
    /// <item><description>реализует <see cref="IHostedService"/>;</description></item>
    /// <item><description>помечен <see cref="InjectAsHostedServiceAttribute"/>.</description></item>
    /// </list>
    /// </summary>
    /// <param name="services">Коллекция сервисов, куда будут добавлены фоновые службы.</param>
    /// <param name="assembly">Сборка, в которой выполняется поиск подходящих типов.</param>
    public static void AddHostedServicesFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        foreach (var hostedServiceType in assembly.GetTypes()
                     .Where(type => type.IsClass
                                    && !type.IsAbstract
                                    && type.IsAssignableTo(typeof(IHostedService))
                                    && type.GetCustomAttribute<InjectAsHostedServiceAttribute>() != null))
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHostedService), hostedServiceType));
        }
    }
        
    /// <summary>
    /// Регистрирует ограничения маршрутов для всех перечислений, найденных в переданных сборках.
    /// Позволяет использовать имена enum напрямую в атрибутах маршрутизации.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="assemblies">Сборки, в которых требуется найти перечисления.</param>
    /// <returns>Исходная коллекция сервисов для построения цепочек вызовов.</returns>
    public static IServiceCollection RegisterEnumRouteConstraints(
        this IServiceCollection services, params Assembly[] assemblies)
    {
        var constraintGenericType = typeof(EnumRouteConstraint<>);

        services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options =>
        {
            foreach (var assembly in assemblies)
            {
                foreach (var enumType in assembly.GetTypes().Where(type => type.IsEnum))
                {
                    Type[] typeArgs = { enumType };
                    var constraintType = constraintGenericType.MakeGenericType(typeArgs);

                    options.ConstraintMap.Add(enumType.Name, constraintType);
                }
            }
        });

        return services;
    }
}
