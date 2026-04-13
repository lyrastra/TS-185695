using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;
using Moedelo.Infrastructure.AspNetCore.Swagger.Filters;

namespace Moedelo.Infrastructure.AspNetCore.Swagger;

internal class MoedeloSwaggerOptionsConfigurator : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private readonly MoedeloSwaggerSettings moedeloSwaggerSettings;

    public MoedeloSwaggerOptionsConfigurator(
        IApiVersionDescriptionProvider provider,
        IOptions<MoedeloSwaggerSettings> moedeloSwaggerSettingsAccessor)
    {
        this.provider = provider;
        this.moedeloSwaggerSettings = moedeloSwaggerSettingsAccessor.Value;
    }

    public void Configure(SwaggerGenOptions options)
    {
        var xmlFilePath = GetXmlCommentsPath(moedeloSwaggerSettings.AppName);
        options.IncludeXmlComments(xmlFilePath);

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Title = moedeloSwaggerSettings.GroupNameTitle,
                    Version = $"v{description.ApiVersion}"
                });
        }

        options.EnableAnnotations();
        options.OperationFilter<RemoveVersionFromParameter>();

        //Подключить пользовательские DocumentFilter'ы, перечисленные в настройках,
        //которые могут изменять документы Swagger после их первоначальной генерации.
        //Например: скрыть приватные эндпойнты
        foreach (var filter in moedeloSwaggerSettings.DocumentFilters)
        {
            options.DocumentFilterDescriptors.Add(new FilterDescriptor
            {
                Type = filter,
                Arguments = Array.Empty<object>()
            });
        }

        options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        options.CustomSchemaIds(x => x.FullName);
        options.SchemaFilter<SwaggerExcludeFilter>();

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        options.AddSecurityDefinition("api-key", new OpenApiSecurityScheme
        {
            Description = "Moedelo api key header. Example: \"md-api-key: {apikey}\"",
            Name = "md-api-key",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                },
                Array.Empty<string>()
            },
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "api-key"}
                },
                Array.Empty<string>()
            },
        };

        options.AddSecurityRequirement(securityRequirement);

        if (moedeloSwaggerSettings.FixEnumComments)
        {
            FixEnumDocumentation(options, xmlFilePath);
        }
    }

    private static void FixEnumDocumentation(SwaggerGenOptions options, string xmlFilePath)
    {
        options.AddEnumsWithValuesFixFilters(o =>
        {
            // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
            o.ApplySchemaFilter = true;

            // alias for replacing 'x-enumNames' in swagger document
            o.XEnumNamesAlias = "x-enum-varnames";

            // alias for replacing 'x-enumDescriptions' in swagger document
            o.XEnumDescriptionsAlias = "x-enum-descriptions";

            // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
            o.ApplyParameterFilter = true;

            // add document filter to fix enums displaying in swagger document
            o.ApplyDocumentFilter = true;

            // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
            o.IncludeDescriptions = true;

            // add remarks for descriptions from xml-comments
            o.IncludeXEnumRemarks = true;

            // get descriptions from DescriptionAttribute then from xml-comments
            o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

            // new line for enum values descriptions
            // o.NewLine = Environment.NewLine;
            o.NewLine = "\n";

            // get descriptions from xml-file comments on the specified path
            // should use "options.IncludeXmlComments(xmlFilePath);" before
            o.IncludeXmlCommentsFrom(xmlFilePath);
            // the same for another xml-files...
        });
    }

    private static string GetXmlCommentsPath(string appName)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        return Path.Combine(baseDirectory, $"{appName}.xml");
    }
}