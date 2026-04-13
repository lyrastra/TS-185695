using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.Finances.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class PingController(IWebAppConfigChecker appConfigChecker) : ApiController
{
    [HttpGet]
    [Route("Ping")]
    public IHttpActionResult Get()
    {
        appConfigChecker.CheckWebAppConfiguration();
        return Ok("pong");
    }
}