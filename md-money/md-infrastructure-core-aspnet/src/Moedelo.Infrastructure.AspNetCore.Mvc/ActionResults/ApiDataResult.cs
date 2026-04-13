using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;

/// <summary>
/// <see cref="IActionResult"/>, который сериализует произвольное значение в обёртку <c>{ data = ... }</c>.
/// Удобно использовать в контроллерах, где требуется единообразный контракт ответа.
/// </summary>
public class ApiDataResult : IActionResult
{
    private readonly object data;

    /// <summary>
    /// Инициализирует экземпляр результата для указанного объекта данных.
    /// </summary>
    /// <param name="data">Произвольный объект, который необходимо вернуть клиенту.</param>
    public ApiDataResult(object data)
    {
        this.data = data;
    }

    /// <summary>
    /// HTTP статус ответа. По умолчанию — 200.
    /// </summary>
    public int StatusCode { get; set; } = 200;

    /// <summary>
    /// Формирует объект ответа и передаёт выполнение инфраструктуре ASP.NET Core.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <returns>Задача, завершаемая после отправки ответа.</returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        var value = new { data };
        var objectResult = new ObjectResult(value)
        {
            StatusCode = StatusCode
        };
            
        return objectResult.ExecuteResultAsync(context);
    }
}