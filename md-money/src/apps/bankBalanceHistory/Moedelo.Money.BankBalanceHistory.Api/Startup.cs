using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.AspNet.Mvc.Extensions;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.Consul.ServiceDiscovery.Extensions;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;

namespace Moedelo.Money.BankBalanceHistory.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMoedeloMvc();
        services.AddApiVersioning(
            options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

        services.AddHttpContextAccessor();
        services.AddMoedeloApiResponseBehavior();

        services.RegisterByDIAttribute("Moedelo.*");

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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

        app.UsePathBase("/MoneyBankBalanceHistory");
        app.LogMoedeloConsulLoadingErrors();
        app.UseRouting();
        app.UseMoedeloCors();
        app.UsePing();
        //app.UseMoedeloSwagger();
        app.UseExecutionInfoContext();
        app.UseAuditApiHandlerTrace();
        app.UseDefaultExceptionHandler();
        app.UseRejectionOfUnauthorizedRequests();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}