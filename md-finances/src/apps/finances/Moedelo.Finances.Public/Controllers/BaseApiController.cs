using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Moedelo.AccountManagement.Public.Infrastructure.Web;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.Finances.Public.Infrastructure.Web;

namespace Moedelo.Finances.Public.Controllers
{
    [NoCache]
    [WebApiRejectUnauthorizedRequest]
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected IHttpActionResult Data(object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new PublicApiDataResult(Request, statusCode, data);
        }

        protected IHttpActionResult ValidationError()
        {
            return new PublicApiValidationErrorResult(Request, ModelState);
        }

        protected IHttpActionResult Error(HttpStatusCode statusCode, params string[] messages)
        {
            return new PublicApiErrorResult(Request, statusCode, messages);
        }

        protected IHttpActionResult Frame(IReadOnlyCollection<object> data, int offset, int limit, int totalCount,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new PublicApiFrameResult(Request, statusCode, data, offset, limit, totalCount);
        }

        protected IHttpActionResult NotFound(string messages, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            return new PublicApiNotFoundResult(Request, statusCode, messages);
        }
    }
}