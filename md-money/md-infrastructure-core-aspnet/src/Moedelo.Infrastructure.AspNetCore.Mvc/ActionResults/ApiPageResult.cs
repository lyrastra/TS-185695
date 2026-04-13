using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;

/// <summary>
/// <see cref="IActionResult"/>, сериализующий коллекцию данных постраничного ответа.
/// Возвращает JSON вида <c>{ data, offset, limit, totalCount }</c>.
/// </summary>
public class ApiPageResult : IActionResult
{
    /// <summary>
    /// HTTP статус ответа. По умолчанию — 200.
    /// </summary>
    public int StatusCode { get; set; } = 200;

    private readonly IReadOnlyCollection<object> data;
    private readonly int offset;
    private readonly int limit;
    private readonly int totalCount;

    /// <summary>
    /// Создаёт новый постраничный результат.
    /// </summary>
    /// <param name="data">Элементы текущей страницы.</param>
    /// <param name="offset">Смещение относительно начала коллекции.</param>
    /// <param name="limit">Размер страницы.</param>
    /// <param name="totalCount">Общее количество элементов, доступных для навигации.</param>
    public ApiPageResult(IReadOnlyCollection<object> data, int offset, int limit, int totalCount)
    {
        this.data = data;
        this.offset = offset;
        this.limit = limit;
        this.totalCount = totalCount;
    }

    /// <summary>
    /// Формирует итоговый JSON и делегирует исполнение ASP.NET Core.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <returns>Задача, завершаемая после отправки ответа.</returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        var value = new
        {
            data,
            offset,
            limit,
            totalCount
        };
        var objectResult = new ObjectResult(value)
        {
            StatusCode = StatusCode
        };

        return objectResult.ExecuteResultAsync(context);
    }
}