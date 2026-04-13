using System;
using System.Linq;
using System.Reflection;
using Swashbuckle.Swagger;

namespace Moedelo.CommonV2.Swagger.SchemaFilters
{
    /// <summary>
    /// Расширение, позволяющее скрывать поля модели в документации атрибутом
    /// </summary>
    public class AddSchemaExcludePropsFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            if (schema?.properties == null || type == null)
                return;

            var excludedProperties = type.GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.properties.ContainsKey(excludedProperty.Name))
                    schema.properties.Remove(excludedProperty.Name);
            }
        }
    }
}