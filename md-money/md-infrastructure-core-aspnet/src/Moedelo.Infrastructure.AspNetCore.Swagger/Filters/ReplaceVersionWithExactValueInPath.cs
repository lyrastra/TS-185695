using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Filters
{
    public sealed class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var path = new OpenApiPaths();

            foreach (var keyValuePair in swaggerDoc.Paths)
            {
                path.Add(keyValuePair.Key.Replace("v{version}", swaggerDoc.Info.Version), keyValuePair.Value);
            }

            swaggerDoc.Paths = path;
        }
    }
}