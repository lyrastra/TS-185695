namespace Moedelo.Billing.Shared.Enums;

public enum BillStatus
{
    /// <summary>
    /// Создан
    /// </summary>
    Created = 1,

    /// <summary>
    /// Выставить не удалось
    /// </summary>
    InvoicingFailed = 2,

    /// <summary>
    /// Выставлен
    /// </summary>
    Invoiced = 3,

    /// <summary>
    /// Оплачен
    /// </summary>
    Paid = 4,
}
