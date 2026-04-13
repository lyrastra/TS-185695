using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Moedelo.InfrastructureV2.WebApi.Validation.Http;
using Moedelo.InfrastructureV2.WebApi.Validation.Utils;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Filters;

public class WebApiValidateModelStateFilterAttribute : ActionFilterAttribute
{
    protected const string ValidationErrors = "ValidationErrors";

    /// <summary>
    /// Должны ли проверяться параметры GET-запросов
    /// С валидацией параметров GET-запросов есть нюансы
    /// Удостоверьтесь, что осознаёте их и принимаете, прежде чем включать валидацию на GET-запросы
    /// (пример: https://stackoverflow.com/questions/12006524/why-does-modelstate-isvalid-fail-for-my-apicontroller-method-that-has-nullable-p/12622152)
    /// </summary>
    public bool ValidateHttpGetParameters { get; set; } = false;

    private readonly bool sendExceptionsInfo;

    public WebApiValidateModelStateFilterAttribute(bool sendExceptionsInfo = false)
    {
        this.sendExceptionsInfo = sendExceptionsInfo;
    }

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        var httpMethod = actionContext.Request.Method;

        if ((httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete) && ValidateHttpGetParameters)
        {
            // для GET-запросов надо проверить валидационные атрибуты, навешанные на параметры запроса
            OnGetActionExecuting(actionContext);
        }
        else if (httpMethod != HttpMethod.Post && httpMethod != HttpMethod.Put)
        {
            return;
        }

        if (!actionContext.ModelState.IsValid)
        {
            var (httpError, hasErrors) = actionContext.ModelState.CreateValidationHttpError(ValidationErrors);

            if (sendExceptionsInfo)
            {
                actionContext.ModelState.AddExceptionsTo(httpError);
            }

            if (!hasErrors)
            {
                httpError.Add("ExtraDevInfo", "To get more info try to use WebApiValidateModelStateFilterAttribute with sendExceptionsInfo=true (look at constructor)");
            }

            actionContext.Response = GetResponse(actionContext, httpError);
        }
    }
        
    private static void OnGetActionExecuting(HttpActionContext context)
    {
        var descriptor = context.ActionDescriptor;

        if (descriptor != null)
        {
            var parameters = context.ActionDescriptor.GetParameters();

            foreach (var parameter in parameters)
            {
                var argument = context.ActionArguments[parameter.ParameterName];

                EvaluateValidationAttributes(parameter, argument, context.ModelState);
            }
        }
    }

    private static void EvaluateValidationAttributes(
        HttpParameterDescriptor parameter,
        object argument,
        ModelStateDictionary modelState)
    {
        var validationAttributes = parameter.GetCustomAttributes<ValidationAttribute>();

        foreach (var attributeData in validationAttributes)
        {
            var isValid = attributeData.IsValid(argument);
            if (!isValid)
            {
                modelState
                    .AddModelError(
                        parameter.ParameterName,
                        attributeData.FormatErrorMessage(parameter.ParameterName));
            }
        }
    }

    protected virtual HttpResponseMessage GetResponse(HttpActionContext actionContext, HttpError httpError)
    {
        return actionContext.Request.CreateErrorResponse(
            HttpStatusCodeEx.ValidationFailed,
            httpError);
    }
}