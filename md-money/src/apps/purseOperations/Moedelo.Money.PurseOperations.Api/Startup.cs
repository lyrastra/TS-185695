using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;
using Moedelo.Money.PurseOperations.Api.Infrastructure;

namespace Moedelo.Money.PurseOperations.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
            });

        services.AddHttpContextAccessor();

        services.RegisterByDIAttribute("Moedelo.*");
        services.AddWarmup();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UsePathBase("/MoneyPurseOperations");
        app.LogMoedeloConsulLoadingErrors();
        app.UseRouting();
        app.UsePing();
        app.UseExecutionInfoContext();
        app.UseAuditApiHandlerTrace(null, null);
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseRejectionOfUnauthorizedRequests();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}