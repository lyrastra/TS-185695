using System;
using Moedelo.Billing.Abstractions.Dto.Gateway.PrimaryBills;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Bills;

/// <summary>
/// Информация о счёте
/// </summary>
public class BillingGatewayBillInfoDto
{
    /// <summary>
    /// Идентификатор истории платежей
    /// </summary>
    public int PaymentHistoryId { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Номер счёта
    /// </summary>
    public string BillNumber { get; set; }

    /// <summary>
    /// Флаг выставления счёта через Новый Биллинг
    /// </summary>
    public bool IsBackofficeBillingBill { get; set; }

    /// <summary>
    /// Источник выставления счёта Нового Билинга. Для счетов Нового Биллинга.
    /// </summary>
    public BillCreationSource? BillCreationSource { get; set; }

    /// <summary>
    /// Тип операции счёта. Для счетов Нового Биллинга.
    /// </summary>
    public BillingOperationType? BillOperationType { get; set; }

    /// <summary>
    /// Инофрмация о ПУ в счёте. Для счетов Нового Биллинга
    /// </summary>
    public BillingGatewayPrimaryBillConfigurationDto[] BillConfigurations { get; set; }

    /// <summary>
    /// Флаг "Внутренний счёт".
    /// </summary>
    public bool IsInnerBill { get; set; }

    /// <summary>
    /// Сумма счёта
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Статус счёта. Для счетов Нового Биллинга
    /// </summary>
    public BillStatus? BillStatus { get; set; }

    /// <summary>
    /// Дата выставления счёта
    /// </summary>
    public DateTime BillDate { get; set; }
}
