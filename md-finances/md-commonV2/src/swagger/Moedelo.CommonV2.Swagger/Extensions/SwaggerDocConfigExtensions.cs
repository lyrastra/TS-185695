using System.Linq;
using System.Net.Http;
using Swashbuckle.Application;

namespace Moedelo.CommonV2.Swagger.Extensions
{
    public static class SwaggerDocConfigExtensions
    {
        /// <summary>
        /// Возвращает RootUrl без порта (подставляется для приложений на .net framework => ломает страницу документации)
        /// </summary>
        public static SwaggerDocsConfig RootUrlNoPort(this SwaggerDocsConfig c)
        {
            c.RootUrl(request =>
            {
                var rootPath = request.GetConfiguration().VirtualPathRoot.TrimEnd('/');
                var scheme = GetScheme(request);
                var host = request.Headers.Host;
                return $"{scheme}://{host}{rootPath}";
            });

            return c;
        }

        private static string GetScheme(HttpRequestMessage request)
        {
            if (!request.Headers.Contains("X-Forwarded-Proto"))
            {
                return request.RequestUri.Scheme;
            }

            var xForwardedProto = request.Headers.GetValues("X-Forwarded-Proto").First();
            xForwardedProto = xForwardedProto.Split(',')[0];
            return xForwardedProto;
        }
    }
}