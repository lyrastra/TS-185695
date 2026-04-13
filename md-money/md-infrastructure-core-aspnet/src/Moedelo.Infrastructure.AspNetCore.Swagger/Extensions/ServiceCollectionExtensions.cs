#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMoedeloSwagger(this IServiceCollection services, string appName, string groupNameTitle,
        Action<MoedeloSwaggerSettings>? configure = null)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, MoedeloSwaggerOptionsConfigurator>();

        services.Configure<MoedeloSwaggerSettings>(settings =>
        {
            settings.AppName = appName;
            settings.GroupNameTitle = groupNameTitle;

            configure?.Invoke(settings);
        });

        services.AddSwaggerGen();
        services.AddSwaggerGenNewtonsoftSupport();
    }
}
