namespace Moedelo.Billing.Shared.Enums.InvoiceBill;

/// <summary>
/// Причина неудачи
/// </summary>
public enum FailReason
{
    /// <summary>
    /// Не определена
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Доступ не разрешен
    /// </summary>
    AccessDenied = 1,

    /// <summary>
    /// Проверка не удалась
    /// </summary>
    ValidationFailed = 2,

    /// <summary>
    /// Внутренняя ошибка сервера
    /// </summary>
    InternalServerError = 3,
}