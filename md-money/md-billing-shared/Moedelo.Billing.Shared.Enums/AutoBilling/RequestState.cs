using System.ComponentModel;

namespace Moedelo.Billing.Shared.Enums.AutoBilling;

public enum RequestState
{
    /// <summary> Создан </summary>
    [Description("Создан запрос на выставление счёта")]
    Created = 0,

    /// <summary> Фирма не валидна </summary>
    [Description("Фирма не валидна")]
    FirmNotValid = 1,

    /// <summary> Счет не валиден </summary>
    [Description("Счет не валиден")]
    BillNotValid = 2,

    /// <summary> Счет выставлен </summary>
    [Description("Счет выставлен")]
    BillCreated = 3,

    /// <summary> Счет не выставлен </summary>
    [Description("Счет не выставлен")]
    BillNotCreated = 4,

    /// <summary> Не найден запуск </summary>
    [Description("Не найден запуск")]
    InitiateNotFound = 5,

    /// <summary> Не найден пользователь </summary>
    [Description("Не найден пользователь")]
    UserNotFound = 6,

    /// <summary> Не найдены ПУ для продления </summary>
    [Description("Не найдены ПУ для продления")]
    ProductConfigurationsNotFound = 7,

    /// <summary> Фирма добавлена в запуск </summary>
    [Description("Фирма добавлена в запуск")]
    RequestAlreadyExists = 8
}