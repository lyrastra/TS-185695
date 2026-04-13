namespace Moedelo.Billing.Shared.Enums.BillManagement;

public enum BillChangingParameterNamesEnum: byte
{
    /// <summary>
    /// Наименование изменяемого параметра счёта
    /// </summary>
    ChangedParameterName = 1,

    /// <summary>
    /// Новое значение изменяемого параметра
    /// </summary>
    NewValue = 2,

    /// <summary>
    /// Информационное сообщение
    /// </summary>
    Message = 3,

    /// <summary>
    /// Отправляемые в BackofficeBilling данные
    /// </summary>
    BackofficeBillingData = 4,

    /// <summary>
    /// Логин оператора, запустившего изменение
    /// </summary>
    AuthorLogin = 5,

    /// <summary>
    /// Текущий модификатор основного счёта
    /// </summary>
    CurrentPrimaryBillModifier = 6,

    /// <summary>
    /// Новый модификатор основного счёта
    /// </summary>
    NewPrimaryBillModifier = 7,

    /// <summary>
    /// Информация о действующей группе функции
    /// </summary>
    CurrentFunctionGroupInfo = 8,

    /// <summary>
    /// Информация о новой группе функций
    /// </summary>
    NewFunctionGroupInfo = 9,
}
