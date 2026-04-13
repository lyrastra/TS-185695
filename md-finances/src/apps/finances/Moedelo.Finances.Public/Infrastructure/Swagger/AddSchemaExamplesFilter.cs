using System;
using Swashbuckle.Swagger;

namespace Moedelo.Finances.Public.Infrastructure.Swagger
{
    public class AddSchemaExamplesFilter : ISchemaFilter
    {
        // todo: move to infrastructure (need Swashbuckle 5.4.0)
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            var exampleMethod = type.GetMethod("GetSwaggerExample");
            if (exampleMethod != null && exampleMethod.IsStatic)
            {
                schema.example = exampleMethod.Invoke(null, null);
            }
        }
    }
}