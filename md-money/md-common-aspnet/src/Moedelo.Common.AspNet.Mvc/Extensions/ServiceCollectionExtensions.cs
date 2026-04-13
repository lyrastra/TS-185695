using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moedelo.Common.Audit.Middleware.Extensions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Moedelo.Common.AspNet.Mvc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddMoedeloMvc(this IServiceCollection services,
        Action<MoedeloMvcOptions>? configureOptions = null)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddCors()
            .AddSingleton(TimeProvider.System)
            .AddMoedeloAuditTrail();

        var options = new MoedeloMvcOptions();
        configureOptions?.Invoke(options);

        return services.AddMoedeloMvcControllers(options);
    }

    private static IMvcBuilder AddMoedeloMvcControllers(this IServiceCollection services, MoedeloMvcOptions options)
    {
        return services
            .AddControllers(options.ApplyToMvcOptions)
            .AddControllersAsSingleton(options.InjectControllersAsSingleton)
            .AddNewtonsoftJson(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = options.UseJsonCamelCasePropertyNames
                    ? new CamelCasePropertyNamesContractResolver()
                    : new DefaultContractResolver();

                if (options.ConvertEnumToString)
                {
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                }
            });
    }

    private static IMvcBuilder AddControllersAsSingleton(this IMvcBuilder builder, bool injectAsSingleton)
    {
        if (injectAsSingleton == false)
        {
            return builder;
        }

        var feature = new ControllerFeature();
        builder.PartManager.PopulateFeature(feature);

        foreach (var type in feature.Controllers.Select(c => c.AsType()))
        {
            builder.Services.TryAddSingleton(type, type);
        }

        builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

        return builder;
    }

    private static void ApplyToMvcOptions(this MoedeloMvcOptions options, MvcOptions rawOptions)
    {
        if (options.RespectBrowserAcceptHeader.HasValue)
        {
            rawOptions.RespectBrowserAcceptHeader = options.RespectBrowserAcceptHeader.Value;
        }
    }
}
