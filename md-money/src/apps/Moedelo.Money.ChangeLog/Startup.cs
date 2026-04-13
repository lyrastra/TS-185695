using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.Consul.ServiceDiscovery.Extensions;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;

namespace Moedelo.Money.ChangeLog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterByDIAttribute("Moedelo.*");
            services.AddMoedeloServiceDiscovery();
            services.AddMoedeloKafkaConsumersMonitoring();

            var backgroundServices = typeof(Startup)
                .Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(BackgroundService)))
                .ToArray();

            var methodInfo = typeof(ServiceCollectionHostedServiceExtensions)
                .GetMethods()
                .Where(m => m.Name == nameof(ServiceCollectionHostedServiceExtensions.AddHostedService))
                .FirstOrDefault(m => m.GetParameters().Length == 1);

            foreach(var backgroundServiceType in backgroundServices)
            {
                var method = methodInfo.MakeGenericMethod(backgroundServiceType);

                method.Invoke(null, new[] {services});
                
                // services.TryAddEnumerable(ServiceDescriptor
                //         .Singleton(
                //             backgroundServiceType,
                //             factory => factory.GetService(backgroundServiceType))
                //     );
            }
            services.AddWarmup();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/MoneyChangeLog");
            app.LogMoedeloConsulLoadingErrors();
            app.UsePing();
            app.UseAuditApiHandlerTrace();
        }
    }
}
