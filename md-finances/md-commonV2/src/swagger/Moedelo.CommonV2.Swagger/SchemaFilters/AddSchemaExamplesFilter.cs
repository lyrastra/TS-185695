using System;
using Swashbuckle.Swagger;

namespace Moedelo.CommonV2.Swagger.SchemaFilters
{
    /// <summary>
    /// Расширение, позволяющее добавлять пример модели в документацию (метод GetSwaggerExample)
    /// </summary>
    public class AddSchemaExamplesFilter : ISchemaFilter
    {
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