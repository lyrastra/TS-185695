using System.ComponentModel;

namespace Moedelo.Billing.Shared.Enums.AutoBilling;

/// <summary> Статус запуска </summary>
public enum InitiateState
{
    /// <summary> Создан </summary>
    [Description("Создан")]
    Created = 0,

    /// <summary> Создание запросов </summary>
    [Description("Создание запросов")]
    CreatingRequests = 1,

    /// <summary> Валидация Фирм </summary>
    [Description("Валидация фирм")]
    ValidatingFirms = 2,

    /// <summary> Валидация Счетов </summary>
    [Description("Валидация счетов")]
    ValidatingBills = 3,

    /// <summary> Генерация Счетов </summary>
    [Description("Генерация счетов")]
    GeneratingBills = 4,

    /// <summary> Закончен </summary>
    [Description("Завершён")]
    Done = 5,

    /// <summary> Отменен </summary>
    [Description("Отменен")]
    Canceled = 6,
}