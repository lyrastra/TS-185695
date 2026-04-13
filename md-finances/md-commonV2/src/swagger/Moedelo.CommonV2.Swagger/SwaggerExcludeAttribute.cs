using System;

namespace Moedelo.CommonV2.Swagger
{
    /// <summary>
    /// Скрывает свойство модели из документации (необходимо включить AddSchemaExcludePropsFilter)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
}