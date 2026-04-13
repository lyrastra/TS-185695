namespace Moedelo.Billing.Shared.Enums.AutoBilling;

public enum AutoBillingQueueLogStatus
{
    /// <summary> Создано </summary>
    Created = 0,

    /// <summary> Валидно </summary>
    Valid = 1,

    /// <summary> Не валидно </summary>
    NotValid = 2,

    /// <summary> Фирма удалена </summary>
    FirmDeleted = 3,
}