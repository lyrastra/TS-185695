namespace Moedelo.Billing.Shared.Enums.InvoiceBill;

public enum BillRequestStatus
{
    /// <summary>
    /// Выполняется 
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Выполнить не удалось
    /// </summary>
    Failed = 2,

    /// <summary>
    /// Завершен успешно
    /// </summary>
    Completed = 3,
}