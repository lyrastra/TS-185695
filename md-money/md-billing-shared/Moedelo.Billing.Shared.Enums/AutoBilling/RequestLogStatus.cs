namespace Moedelo.Billing.Shared.Enums.AutoBilling;

public enum RequestLogStatus
{
    /// <summary> Неизвестно </summary>
    Undefined = 0,

    /// <summary> Создано </summary>
    Created = 1,

    /// <summary> Фирма валидна </summary>
    FirmValid = 2,

    /// <summary> Фирма не валидна </summary>
    FirmNotValid = 3,

    /// <summary> Счет валиден </summary>
    BillValid = 4,

    /// <summary> Счет не валиден </summary>
    BillNotValid = 5,

    /// <summary> Счет выставлен </summary>
    BillCreated = 6,

    /// <summary> Счет не выставлен </summary>
    BillNotCreated = 7,
}