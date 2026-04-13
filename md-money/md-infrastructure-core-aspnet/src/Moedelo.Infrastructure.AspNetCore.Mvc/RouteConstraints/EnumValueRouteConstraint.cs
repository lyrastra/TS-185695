using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Moedelo.Infrastructure.AspNetCore.Mvc.RouteConstraints;

/// <summary>
/// Constraint для маршрутизации enum'ов, поддерживающий строковые и числовые представления значения.
/// </summary>
public class EnumRouteConstraint<TEnum> : IRouteConstraint where TEnum : struct, Enum
{
    /// <summary>
    /// Проверяет, что значение из маршрута соответствует перечислению <typeparamref name="TEnum"/>.
    /// </summary>
    /// <param name="httpContext">Текущий контекст HTTP-запроса.</param>
    /// <param name="route">Маршрут, внутри которого выполняется проверка.</param>
    /// <param name="routeKey">Имя параметра маршрута.</param>
    /// <param name="values">Коллекция значений маршрута.</param>
    /// <param name="routeDirection">Направление обработки (генерация URL или распознавание маршрута).</param>
    /// <returns><c>true</c>, если значение соответствует одному из элементов перечисления; иначе <c>false</c>.</returns>
    public bool Match(HttpContext? httpContext,
        IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && value != null)
        {
            var stringValue = value.ToString();
            // Поддерживаем как строковые значения, так и числовые
            return Enum.TryParse<TEnum>(stringValue, ignoreCase: true, out _) ||
                   (int.TryParse(stringValue, out var intValue) && Enum.IsDefined(typeof(TEnum), intValue));
        }
        return false;
    }
}
