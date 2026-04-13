using System;

namespace Moedelo.Infrastructure.AspNetCore.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }
}
