using Microsoft.Extensions.DependencyInjection;
using Moedelo.Infrastructure.AspNetCore.HostedServices;
using System;

namespace Moedelo.Infrastructure.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет фоновый сервис <see cref="QueuedHostedService"/>
        /// </summary>
        public static IServiceCollection AddQueuedHostedService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return services.AddHostedService<QueuedHostedService>();
        }

        /// <summary>
        /// Добавляет реализацию фонового сервиса типа <see cref="PeriodicHostedService"/>
        /// </summary>
        [Obsolete("Используй AddPeriodicHostedService из Moedelo.Common.AspNet.HostedServices (сабмодуль md-common-aspnet)")]
        public static IServiceCollection AddPeriodicHostedService<T>(this IServiceCollection services)
            where T: PeriodicHostedService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return services.AddHostedService<T>();
        }
    }
}