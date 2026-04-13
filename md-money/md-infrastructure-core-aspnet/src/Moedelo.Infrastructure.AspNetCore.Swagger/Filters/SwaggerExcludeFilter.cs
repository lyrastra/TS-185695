using Moedelo.Infrastructure.AspNetCore.Swagger.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Filters
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.Type == null)
            {
                return;
            }

            var excludedProperties = context.Type
                .GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null)
                .ToArray();

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty.Name))
                {
                    schema.Properties.Remove(excludedProperty.Name);
                }
            }
        }
    }
}