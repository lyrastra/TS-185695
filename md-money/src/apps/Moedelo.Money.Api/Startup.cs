using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;
using Moedelo.Infrastructure.AspNetCore.Swagger.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;
using Moedelo.Money.Api.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Moedelo.Money.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");
        services.AddApiVersioning(
            options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

        services.AddMoedeloSwagger("Moedelo.Money.Api", "Money operation api");
        services.AddHttpContextAccessor();
        services.AddMoedeloApiResponseBehavior();

        services.RegisterByDIAttribute("Moedelo.*");

        services.AddQueuedHostedService();
        services.AddWarmup();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UsePathBase("/Money");
        app.LogMoedeloConsulLoadingErrors();
        app.UseRouting();
        app.UseMoedeloCors();
        app.UsePing();
        app.UseMoedeloSwagger();
        app.UseExecutionInfoContext();
        app.UseAuditApiHandlerTrace(null, null);
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseRejectionOfUnauthorizedRequests();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}