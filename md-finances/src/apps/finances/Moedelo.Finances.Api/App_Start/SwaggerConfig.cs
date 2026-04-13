using System;
using System.IO;
using System.Web.Http;
using WebActivatorEx;
using Moedelo.Finances.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Moedelo.Finances.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Moedelo.Finances.Api");

                    var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
                    var commentsFileName = thisAssembly.GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.IncludeXmlComments(commentsFile);
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}
