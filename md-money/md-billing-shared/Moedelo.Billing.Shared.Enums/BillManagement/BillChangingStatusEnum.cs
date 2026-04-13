namespace Moedelo.Billing.Shared.Enums.BillManagement;

public enum BillChangingStatusEnum: byte
{
    /// <summary>
    /// Процесс изменения параметров счёта запущен
    /// </summary>
    Started = 1,

    /// <summary>
    /// Серверная ошибка
    /// </summary>
    Error = 2,

    /// <summary>
    /// Обновление заврешилось с ошибкой. Счёт не может быть изменён согласно бизнес-правилам
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Отправка данных в BackofficeBilling
    /// </summary>
    SendingBackofficeBillingData = 4,

    /// <summary>
    /// Изменение успешно завершено
    /// </summary>
    Success = 5,

    /// <summary>
    /// Обработка запроса на стороне BackofficeBilling
    /// </summary>
    BackofficeBillingProcessing = 6,
}
