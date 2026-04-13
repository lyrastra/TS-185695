using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;

/// <summary>
/// <see cref="IActionResult"/> для единообразного возврата ошибок валидации.
/// Преобразует <see cref="ModelStateDictionary"/> в JSON вида <c>{ errors }</c>.
/// </summary>
public class ApiValidationErrorResult : IActionResult
{
    private readonly ModelStateDictionary modelState;

    /// <summary>
    /// Создаёт результат на основе состояния модели.
    /// </summary>
    /// <param name="modelState">Состояние модели, содержащее сообщения об ошибках.</param>
    public ApiValidationErrorResult(ModelStateDictionary modelState)
    {
        this.modelState = modelState;
    }

    /// <summary>
    /// HTTP статус ответа. По умолчанию — 422 (Unprocessable Entity).
    /// </summary>
    public int StatusCode { get; set; } = 422;

    /// <summary>
    /// Преобразует ошибки модели в объект ответа и делегирует исполнение ASP.NET Core.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <returns>Задача, завершаемая после отправки ответа.</returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        IDictionary<string, object?> errors = new ExpandoObject();

        foreach (var (key, value) in modelState)
        {
            var messages = value.Errors
                .Where(e => !string.IsNullOrEmpty(e.ErrorMessage))
                .Select(x => x.ErrorMessage);
            errors.Add(key, messages);
        }

        var data = new { errors };
        var objectResult = new ObjectResult(data)
        {
            StatusCode = StatusCode
        };

        return objectResult.ExecuteResultAsync(context);
    }
}