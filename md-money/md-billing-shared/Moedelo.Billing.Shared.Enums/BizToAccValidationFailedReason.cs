namespace Moedelo.Billing.Shared.Enums;

public enum BizToAccValidationFailedReason
{
    /// <summary>
    /// Нет платежей
    /// </summary>
    NoAnyPayment = 1,

    /// <summary>
    /// Действующий платеж является триалом
    /// </summary>
    OnTrial = 2,

    /// <summary>
    /// Действующий платеж является истекшим
    /// </summary>
    ExpiredPayment = 3,

    /// <summary>
    /// Действующий платеж не на платформе "БИЗ"
    /// </summary>
    NotOnBizTariff = 4,

    /// <summary>
    /// Есть платеж, который невозможно перевести (вероятно, требуется настройка drools, см. логи)
    /// </summary>
    HasNotTransferablePayment = 5,

    /// <summary>
    /// Есть платеж, для которого не найдено однозначного соответствия новой ПУ/тарифу (требуется настройка drools)
    /// </summary>
    HasPaymentWithoutNewTariff = 6,

    /// <summary>
    /// Есть платеж с несколькими ПУ (требуется доработка)
    /// </summary>
    HasMultipleConfigurationsPayment = 7,
}