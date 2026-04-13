using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.AspNet.Mvc.Extensions;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.ExecutionContext.Middleware;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.AspNetCore.HostedServices;
using Moedelo.Infrastructure.AspNetCore.Mvc.Extensions;
using Moedelo.Infrastructure.AspNetCore.Swagger.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;

namespace Moedelo.Money.Reports.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMoedeloMvc();

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                });

            services.AddMoedeloSwagger("Moedelo.Money.Reports.Api", "Money reports api");

            services.AddHttpContextAccessor();
            services.AddMoedeloApiResponseBehavior();

            services.RegisterByDIAttribute("Moedelo.*");
            services.AddWarmup();

            services.AddHostedService<QueuedHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/MoneyReports");
            app.LogMoedeloConsulLoadingErrors();
            app.UsePing();
            app.UseRouting();
            app.UseMoedeloCors();
            app.UseMoedeloSwagger();
            app.UseExecutionInfoContext();
            app.UseAuditApiHandlerTrace();
            app.UseDefaultExceptionHandler();
            //app.UseRejectionOfUnauthorizedRequests();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
