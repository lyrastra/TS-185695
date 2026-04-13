namespace Moedelo.Billing.Shared.Enums;

public enum YaPayOrderStatus
{
    /// <summary> Статус не установлен </summary>
    None = 0,
    /// <summary>
    /// Оплата еще в процессе, нужно запросить статус платежа позже. В нотификации этот статус не отправляется.
    /// </summary>
    Pending = 1,
    /// <summary> Оплата совершена. Терминальный успешный статус. </summary>
    Captured = 2,
    /// <summary> Оплата завершилась неудачно. Терминальный неуспешный статус. </summary>
    Failed = 3,
    /// <summary> Превышено время опроса статуса заказа </summary>
    PollingTimeout = 4,
    /// <summary> Не используются в одностадийном сценарии </summary>
    Authorized = 5,
    /// <summary> Не используются в одностадийном сценарии </summary>
    Voided = 6,
    /// <summary> Возврат совершен. Терминальный успешный статус. </summary>
    Refunded = 7,
    /// <summary> Статус "подтвержден", он не используется в одностадийном сценарии оплаты </summary>
    Confirmed = 8,
    /// <summary> Частичный возврат совершен. Терминальный успешный статус. </summary>
    PartiallyRefunded = 9,
}