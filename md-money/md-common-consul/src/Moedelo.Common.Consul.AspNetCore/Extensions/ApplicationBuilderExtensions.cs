using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Moedelo.Common.Consul.AspNetCore.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void LogMoedeloConsulLoadingErrors(this IApplicationBuilder app)
    {
        _ = app.ApplicationServices.GetService<ConsulLoadingErrorLogger>();
    }
}