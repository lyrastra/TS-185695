using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using WebActivatorEx;
using Moedelo.Finances.WebApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Moedelo.Finances.WebApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            if (Global.DebugMode == false)
            {
                return;
            }

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Moedelo.Finances.WebApp");

                    var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
                    var commentsFileName = thisAssembly.GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.Last());
                    c.IncludeXmlComments(commentsFile);
                    c.DescribeAllEnumsAsStrings();
                    c.IgnoreObsoleteActions();
                })
                .EnableSwaggerUi(c =>
                {
                });
        }
    }
}
