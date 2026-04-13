using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Moedelo.InfrastructureV2.WebApi.Validation.Attributes;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Utils;

public static class ModelStateDictionaryExtensions
{
    public static (HttpError httpError, bool hasErrors) CreateValidationHttpError(this ModelStateDictionary modelStateDictionary,
        string validationFieldName = "ValidationErrors")
    {
        var validationErrors = modelStateDictionary.GetValidationErrorsAsHttpError();

        var httpError = new HttpError("Неверный формат запроса или неверные значения параметров. Обратитесь к документации")
        {
            [validationFieldName] = validationErrors
                .OrderBy(p => p.Key)
                .ToDictionary(p => p.Key, p => p.Value)
        };

        return (httpError, validationErrors.Count > 0);
    }

    public static void AddExceptionsTo(this ModelStateDictionary modelStateDictionary, HttpError httpError)
    {
        var debugInfo = new HttpError();
        var validationExceptions = new HttpError();
        debugInfo.Add("ValidationExceptions", validationExceptions);
        httpError.Add("DebugInfo", debugInfo);

        foreach(var pair in modelStateDictionary)
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

    public static HttpError GetValidationErrorsAsHttpError(this ModelStateDictionary modelStateDictionary)
    {
        var validationErrors = new HttpError();

        foreach (var pair in modelStateDictionary.Where(p => p.Value.Errors?.Any() == true))
        {
            var modelState = pair.Value;

            var propName = Regex.Replace(pair.Key, @"\w+\.(.*)", "$1");

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

        foreach(var msg in errorsCollection.Select(e => e.Exception?.Message).Where(m => !string.IsNullOrEmpty(m)))
        {
            foreach (var item in ExceptionToValidationErrorMap)
            {
                var m = item.Regex.Match(msg);

                if (m.Success)
                {
                    if (m.Groups.Count == 2)
                    {
                        var pathOrName = m.Groups[1].ToString();
                        var name = item.Nested ? $"{propName}.{pathOrName}" : pathOrName;
                        validationErrors[name] = item.ValidationError;
                    }
                    else if (m.Groups.Count == 3)
                    {
                        var name = $"{m.Groups[2]}.{m.Groups[1]}";
                        validationErrors[name] = item.ValidationError;
                    }
                }
            }
        }

        return validationErrors;
    }
    
    // список исключений, которые следует преобразовывать в ошибки валидации
    private static readonly ExceptionToValidationError[] ExceptionToValidationErrorMap =
    {
        // ошибка формата даты
        new ExceptionToValidationError
        {
            Regex = new Regex(@"Error reading date.*Path '(.+?)'.*"),
            ValidationError = new[] {"Неверный формат даты"}
        },

        // ошибка формата - недопустимое пустое значение
        new ExceptionToValidationError
        {
            Regex = new Regex(@"Error converting value {null} to type '.+?'\. Path '(.+?)'.*"),
            ValidationError = new[] {"Пустое значение недопустимо"}
        },
        // Не nullable [Required] поля генерируют исключение, но не добавляют validation error
        new ExceptionToValidationError
        {
            Regex = new Regex(@"Required property '(.+?)' not found in JSON. Path '(.+?)'.*"),
            ValidationError = new[] {RequiredValueAttribute.Message},
            Nested = true
        }
    };

    private class ExceptionToValidationError
    {
        /// <summary>
        ///     Регулярное выражение для определения сообщения об исключении
        ///     Регулярное выражение должно содержать одну группу с именем поля
        /// </summary>
        public Regex Regex { get; set; }

        public string[] ValidationError { get; set; }

        /// <summary>
        ///     признак того, что в сообщении об исключении указано имя не самого объекта, а его подполя
        /// </summary>
        public bool Nested { get; set; }
    }
}
