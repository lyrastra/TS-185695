using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Moedelo.InfrastructureV2.WebApi.Validation.Http;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Filters
{
    public class WebApiValidateModelStateFilterV2Attribute : WebApiValidateModelStateFilterAttribute
    {
        protected override HttpResponseMessage GetResponse(HttpActionContext actionContext, HttpError httpError)
        {
            var validationErrorFields = httpError.GetPropertyValue<object>(ValidationErrors);

            return actionContext.Request.CreateResponse(
                HttpStatusCodeEx.ValidationFailed,
                new { errors = validationErrorFields });
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var httpMethod = actionContext.Request.Method;
            if (httpMethod != HttpMethod.Post && httpMethod != HttpMethod.Put)
            {
                return;
            }

            if (actionContext.ActionArguments.ContainsKey("body") == false || actionContext.ActionArguments["body"] == null)
            {
                var httpError = new HttpError("Неверный формат запроса или неверные значения параметров. Обратитесь к документации");
                httpError.Add(ValidationErrors, new { body = "Тело запроса не заполнено" });
                actionContext.Response = GetResponse(actionContext, httpError);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}