using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.Mvc.Internals;

/// <summary>
/// Описание правила обработки
/// </summary>
/// <param name="Regex">
/// Регулярное выражение для определения сообщения об исключении.
/// Регулярное выражение должно содержать одну группу с именем поля
/// </param>
/// <param name="ValidationError"></param>
/// <param name="Nested">
/// Признак того, что в сообщении об исключении указано имя не самого объекта, а его подполя
/// </param>
internal record ExceptionToValidationError(
    Regex Regex,
    string[] ValidationError,
    bool Nested = false);
