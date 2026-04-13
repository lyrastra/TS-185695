namespace Moedelo.Billing.Shared.Enums;

/// <summary>
/// Статус доступности продления
/// </summary>
public enum ProlongationAvailabilityStatus : byte
{
    /// <summary>
    /// Продление доступно
    /// </summary>
    Allowed = 0,
    /// <summary>
    /// Продление недоступно
    /// </summary>
    NotAllowed = 1,
    /// <summary>
    /// Продление недоступно, расчёт параметров тарификации не выполнен
    /// </summary>
    TarifficationNotSuccess = 2,
    /// <summary>
    /// Продление недоступно, оплаченный период истек
    /// </summary>
    NoPaidSubscription = 3,
    /// <summary>
    /// Недоступно самостоятельное продление
    /// </summary>
    SelfProlongationNotAvailable = 4,
    /// <summary>
    /// Продуктовая услуга в архиве
    /// </summary>
    ProductConfigurationInArchive = 5,
    /// <summary>
    /// Недоступна операция Продление
    /// </summary>
    ProlongationOperationNotAvailable = 6,
}