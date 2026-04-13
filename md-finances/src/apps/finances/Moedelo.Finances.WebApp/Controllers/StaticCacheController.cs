using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Moedelo.CommonV2.Webpack.Services;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.Finances.WebApp.Controllers
{
    [RoutePrefix("StaticCache")]
    public class StaticCacheController : ApiController
    {
        private readonly IWebpackService webpackService;

        public StaticCacheController(IWebpackService webpackService)
        {
            this.webpackService = webpackService;
        }

        [HttpGet]
        [Route("Clear")]
        public HttpResponseMessage Clear()
        {
            webpackService.ClearCache();

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpGet]
        [Route("Version")]
        public HttpResponseMessage Version()
        {
            var versions = webpackService.GetVersions();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(versions.ToJsonString(), Encoding.UTF8, "application/json");

            return response;
        }
    }
}
