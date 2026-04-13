using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.Finances.WebApp.Infrastructure.WebApi;

namespace Moedelo.Finances.WebApp.Controllers
{
    [NoCache]
    [WebApiRejectUnauthorizedRequest]
    public class BaseApiController : ApiController
    {
        protected const int UnprocessableEntityCode = 422;
    }
}