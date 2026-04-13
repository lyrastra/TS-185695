using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.Audit.Middleware.Filters;

namespace Moedelo.Common.Audit.Middleware.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMoedeloAuditTrail(this IServiceCollection services)
    {
        services.PostConfigure<MvcOptions>(options =>
        {
            options.Filters.Add(new MoedeloAuditTrailActionFilterAttribute
            {
                Order = 1
            });
        });

        return services;
    }
}
