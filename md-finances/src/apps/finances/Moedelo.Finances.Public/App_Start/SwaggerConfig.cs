using System.Web.Http;
using WebActivatorEx;
using Moedelo.Finances.Public;
using Swashbuckle.Application;
using Moedelo.Finances.Public.Infrastructure.Swagger;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Moedelo.Finances.Public
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                 .EnableSwagger(c =>
                 {
                     c.SingleApiVersion("v1", "SwaggerApi");
                     c.IncludeXmlComments(string.Format(@"{0}\bin\Moedelo.Finances.Public.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                     c.SchemaFilter<AddSchemaExamplesFilter>();
                 })
                 .EnableSwaggerUi(c =>
                 {
                     c.DisableValidator();
                     c.EnableApiKeySupport("md-api-key", "header");
                 });

        }
    }
}
