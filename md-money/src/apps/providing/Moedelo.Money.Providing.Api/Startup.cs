using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.Consul.ServiceDiscovery.Extensions;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;
using Newtonsoft.Json.Serialization;

namespace Moedelo.Money.Providing.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        services.AddApiVersioning(
            options => { options.ReportApiVersions = true; });

        services.AddHttpContextAccessor();
        services.AddMoedeloApiResponseBehavior();

        services.RegisterByDIAttribute("Moedelo.*");

        services.AddMoedeloServiceDiscovery();
        services.AddMoedeloKafkaConsumersMonitoring();
        services.AddHostedServicesFromAssembly(GetType().Assembly);
        services.AddWarmup();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UsePathBase("/MoneyProviding");
        app.LogMoedeloConsulLoadingErrors();
        app.UseRouting();
        app.UsePing();
        app.UseExecutionInfoContext();
        app.UseAuditApiHandlerTrace(null, null);
        app.UseDefaultExceptionHandler();
        app.UseRejectionOfUnauthorizedRequests();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}