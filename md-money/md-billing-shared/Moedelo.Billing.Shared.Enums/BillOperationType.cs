namespace Moedelo.Billing.Shared.Enums;

/// <summary> Типы операций счета </summary>
public enum BillOperationType
{
    /// <summary> Неопределен </summary>
    Undefined = 0,

    /// <summary> Покупка </summary>
    Buy = 1,

    /// <summary> Продление </summary>
    Prolongation = 2,

    /// <summary> Допродажа </summary>
    Reselling = 4,

    /// <summary> Изменение (смена тарифа) </summary>
    Changing = 8,
}