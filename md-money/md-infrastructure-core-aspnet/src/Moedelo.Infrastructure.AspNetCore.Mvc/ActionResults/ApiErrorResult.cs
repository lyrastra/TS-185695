using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;

/// <summary>
/// <see cref="IActionResult"/>, возвращающий массив сообщений об ошибках в формате,
/// совместимом с API МоёДело (<c>{ errors: { messages: [] } }</c>).
/// </summary>
public class ApiErrorResult : IActionResult
{
    private readonly string[] messages;

    /// <summary>
    /// Создаёт результат с указанными сообщениями ошибок.
    /// </summary>
    /// <param name="messages">Список человеко-читаемых описаний ошибки.</param>
    public ApiErrorResult(params string[] messages)
    {
        this.messages = messages;
    }

    /// <summary>
    /// HTTP статус ответа. По умолчанию — 200.
    /// </summary>
    public int StatusCode { get; set; } = 200;

    /// <summary>
    /// Формирует объект ошибки и делегирует исполнение инфраструктуре ASP.NET Core.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <returns>Задача, завершаемая после отправки ответа.</returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        var data = new { errors = new { messages } };
        var objectResult = new ObjectResult(data)
        {
            StatusCode = StatusCode
        };
            
        return objectResult.ExecuteResultAsync(context);
    }
}