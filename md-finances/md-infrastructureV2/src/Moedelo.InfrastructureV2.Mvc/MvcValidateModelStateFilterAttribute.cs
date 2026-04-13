using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Moedelo.InfrastructureV2.Mvc.Internals;

namespace Moedelo.InfrastructureV2.Mvc;

public sealed class MvcValidateModelStateFilterAttribute : ActionFilterAttribute
{
    private const string ValidationErrors = "ValidationErrors";
    private const string ErrorMessage = "Неверный формат запроса или неверные значения параметров. Обратитесь к документации";

    /// <summary>
    /// Должны ли проверяться query-параметры GET- и DELETE-запросов.
    /// ВНИМАНИЕ: query-параметры не валидируются для POST и PUT запросов даже при включенной настройке.
    /// С валидацией параметров GET-запросов есть нюансы
    /// Удостоверьтесь, что осознаёте их и принимаете, прежде чем включать валидацию на GET-запросы
    /// (пример: https://stackoverflow.com/questions/12006524/why-does-modelstate-isvalid-fail-for-my-apicontroller-method-that-has-nullable-p/12622152)
    /// </summary>
    public bool ValidateHttpGetParameters { get; set; } = false;

    // список исключений, которые следует преобразовывать в ошибки валидации
    private static readonly ExceptionToValidationError[] ExceptionToValidationErrorMap =
    {
        // ошибка формата даты
        new ExceptionToValidationError(
            new Regex(@"Error reading date.*Path '(.+?)'.*", RegexOptions.Compiled),
            ["Неверный формат даты"]),

        // ошибка формата - недопустимое пустое значение
        new ExceptionToValidationError(
            new Regex(@"Error converting value {null} to type '.+?'\. Path '(.+?)'.*", RegexOptions.Compiled),
            ["Пустое значение недопустимо"]),
        // Не nullable [Required] поля генерируют исключение, но не добавляют validation error
        new ExceptionToValidationError(
            new Regex(@"Required property '(.+?)' not found in JSON. Path '(.+?)'.*", RegexOptions.Compiled),
            ["Это поле должно быть заполнено"],
            Nested: true)
    };

    private readonly bool sendExceptionsInfo;

    public MvcValidateModelStateFilterAttribute(bool sendExceptionsInfo = false)
    {
        this.sendExceptionsInfo = sendExceptionsInfo;
    }

    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var httpMethod = actionContext.HttpContext.Request.HttpMethod;

        if ((httpMethod == HttpMethod.Get.Method || httpMethod == HttpMethod.Delete.Method) && ValidateHttpGetParameters)
        {
            // для GET-запросов надо проверить валидационные атрибуты, навешанные на параметры запроса
            OnGetActionExecuting(actionContext);
        }
        else if (httpMethod != HttpMethod.Post.Method && httpMethod != HttpMethod.Put.Method)
        {
            return;
        }

        if (actionContext.Controller.ViewData.ModelState.IsValid)
        {
            // данные прошли валидацию
            return;
        }

        var validationErrors = GetValidationErrors(actionContext);
        
        var httpError = new HttpError(ErrorMessage)
        {
            [ValidationErrors] = validationErrors
                .OrderBy(kv => kv.Key)
                .ToDictionary(kv => kv.Key, kv => kv.Value)
        };

        if (sendExceptionsInfo)
        {
            CollectExceptions(httpError, actionContext);
        }

        if (!validationErrors.Any())
        {
            httpError.Add("ExtraDevInfo",
                "To get more info try to use MvcValidateModelStateFilterAttribute with sendExceptionsInfo=true (look at constructor)");
        }

        actionContext.Result = new ValidationFailedJsonActionResult(httpError);
    }

    private static void OnGetActionExecuting(ActionExecutingContext actionContext)
    {
        var descriptor = actionContext.ActionDescriptor;

        if (descriptor != null)
        {
            var modelState = actionContext.Controller.ViewData.ModelState;
            var parameters = actionContext.ActionDescriptor.GetParameters();

            foreach (var parameter in parameters)
            {
                var argument = actionContext.ActionParameters[parameter.ParameterName];

                EvaluateValidationAttributes(parameter, argument, modelState);
            }
        }
    }

    private static void EvaluateValidationAttributes(
        ParameterDescriptor parameter,
        object argument,
        ModelStateDictionary modelState)
    {
        var validationAttributes = parameter
            .GetCustomAttributes(typeof(ValidationAttribute), true)
            .Where(attribute => attribute is ValidationAttribute)
            .Cast<ValidationAttribute>();

        foreach (var attributeData in validationAttributes)
        {
            var isValid = attributeData.IsValid(argument);

            if (!isValid)
            {
                var parameterName = parameter.ParameterName;
                
                var errorValue = attributeData.FormatErrorMessage(parameterName);
                
                modelState.AddModelError(parameterName, errorValue);
            }
        }
    }

    private static HttpError GetValidationErrors(ActionExecutingContext actionContext)
    {
        var validationErrors = new HttpError();

        var errors = actionContext.Controller.ViewData.ModelState
            .Where(p => p.Value.Errors?.Any() == true);

        foreach (var errorModelState in errors)
        {
            var propName = Regex.Replace(errorModelState.Key, @"\w+\.(.*)", "$1");
            var modelState = errorModelState.Value;

            var errorStrings = modelState.Errors
                .Where(e => !string.IsNullOrEmpty(e.ErrorMessage))
                .Select(e => e.ErrorMessage)
                .ToArray();

            if (errorStrings.Any())
            {
                validationErrors[propName] = errorStrings;
            }

            foreach (var p in ExtractValidationErrorsFromExceptions(propName, modelState.Errors))
            {
                validationErrors[p.Key] = p.Value;
            }
        }

        return validationErrors;
    }

    private static Dictionary<string, string[]> ExtractValidationErrorsFromExceptions(
        string propName,
        ModelErrorCollection errorsCollection)
    {
        var validationErrors = new Dictionary<string, string[]>();

        foreach (var msg in errorsCollection
                     .Select(modelError => modelError.Exception?.Message)
                     .Where(message => !string.IsNullOrEmpty(message)))
        {
            foreach (var errorConfig in ExceptionToValidationErrorMap)
            {
                var match = errorConfig.Regex.Match(msg!);

                if (!match.Success)
                {
                    continue;
                }

                switch (match.Groups.Count)
                {
                    case 2:
                    {
                        var pathOrName = match.Groups[1].ToString();
                        var name = errorConfig.Nested ? $"{propName}.{pathOrName}" : pathOrName;
                        validationErrors[name] = errorConfig.ValidationError;
                        break;
                    }
                    case 3:
                    {
                        var name = $"{match.Groups[2]}.{match.Groups[1]}";
                        validationErrors[name] = errorConfig.ValidationError;
                        break;
                    }
                }
            }
        }

        return validationErrors;
    }

    private static void CollectExceptions(HttpError httpError, ActionExecutingContext actionContext)
    {
        var debugInfo = new HttpError();
        var validationExceptions = new HttpError();
        debugInfo.Add("ValidationExceptions", validationExceptions);
        httpError.Add("DebugInfo", debugInfo);

        foreach (var pair in actionContext.Controller.ViewData.ModelState)
        {
            var modelState = pair.Value;

            if (modelState.Errors?.Count > 0)
            {
                var exceptions = modelState.Errors
                    .Where(e => !string.IsNullOrEmpty(e.Exception?.Message))
                    .Select(e => e.Exception.Message).ToArray();
                if (exceptions.Any())
                {
                    validationExceptions.Add(Regex.Replace(pair.Key, @"\w+\.(.*)", "$1"), exceptions);
                }
            }
        }
    }
}