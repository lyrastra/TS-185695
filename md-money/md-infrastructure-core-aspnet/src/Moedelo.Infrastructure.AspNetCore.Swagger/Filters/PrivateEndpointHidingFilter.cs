using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Filters;

/// <summary>
/// Фильтр для скрытия приватных эндпоинтов в Swagger-документации
/// </summary>
/// <remarks>эндпойнт считается приватным, если в пути до ресурса указаны сегменты "/private/api"</remarks>
public sealed class PrivateEndpointHidingFilter: IDocumentFilter
{
    private const string privatePathSegmentValue = "/private/api";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var privatePaths = swaggerDoc.Paths.Keys
            .Where(p => p.StartsWith(privatePathSegmentValue, StringComparison.OrdinalIgnoreCase));

        foreach (var path in privatePaths)
        {
            swaggerDoc.Paths.Remove(path);
        }
    }
}