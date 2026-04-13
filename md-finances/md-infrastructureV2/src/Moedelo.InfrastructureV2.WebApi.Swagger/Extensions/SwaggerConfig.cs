using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Swashbuckle.Application;

namespace Moedelo.InfrastructureV2.WebApi.Swagger.Extensions;

public static class SwaggerConfig
{
    public static void ConfigureSwaggerForPublicApi(this HttpConfiguration configuration,
        string swaggerTitle, Assembly webAppAssembly,
        Action<SwaggerSettings>? configure = null)
    {
        configuration.ConfigureSwagger(swaggerTitle, webAppAssembly, settings =>
        {
            settings.DisableUiValidator = false;
            settings.EnableMdApiKey = false;
            configure?.Invoke(settings);
        });
    }
    
    public static void ConfigureSwaggerForExternalApi(this HttpConfiguration configuration,
        string swaggerTitle, Assembly webAppAssembly,
        Action<SwaggerSettings>? configure = null)
    {
        configuration.ConfigureSwagger(swaggerTitle, webAppAssembly, settings =>
        {
            settings.DisableUiValidator = true;
            settings.EnableMdApiKey = true;
            configure?.Invoke(settings);
        });
    }
    
    public static void ConfigureSwaggerForPrivateApi(this HttpConfiguration configuration,
        string swaggerTitle, Assembly webAppAssembly,
        Action<SwaggerSettings>? configure = null)
    {
        configuration.ConfigureSwagger(swaggerTitle, webAppAssembly, settings =>
        {
            settings.DisableUiValidator = false;
            settings.EnableMdApiKey = false;
            configure?.Invoke(settings);
        });
    }

    public static void ConfigureSwagger(this HttpConfiguration configuration,
        string swaggerTitle, Assembly webAppAssembly,
        Action<SwaggerSettings>? configure = null)
    {
        var defaultBaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        var commentsFileName = webAppAssembly.GetName().Name + ".xml";
        var commentFilePath = Path.Combine(defaultBaseDirectory, commentsFileName);
        var settings = new SwaggerSettings(commentFilePath);

        configure?.Invoke(settings);

        configuration
            .EnableSwagger(docsConfig =>
            {
                docsConfig.SingleApiVersion(settings.Version, swaggerTitle);
                docsConfig.IncludeXmlComments(settings.CommentsXmlFilePath);
                docsConfig.DescribeAllEnumsAsStrings();
                docsConfig.UseFullTypeNameInSchemaIds();
                docsConfig.IgnoreObsoleteActions();
            })
            .EnableSwaggerUi(uiConfig =>
            {
                if (settings.DisableUiValidator)
                {
                    uiConfig.DisableValidator();
                }
                if (settings.EnableMdApiKey)
                {
                    uiConfig.EnableApiKeySupport(name: "md-api-key", apiKeyIn: "header");
                }
            });
    }
}
