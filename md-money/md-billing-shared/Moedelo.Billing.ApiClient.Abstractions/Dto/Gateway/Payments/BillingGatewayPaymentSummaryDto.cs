using System;
using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Payments;

/// <summary>
/// Информация о платеже
/// </summary>
public class BillingGatewayPaymentSummaryDto
{
    /// <summary>
    /// Идентификатор платежа
    /// </summary>
    public int PaymentHistoryId { get; set; }

    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Метод оплаты
    /// </summary>
    public string PaymentMethod { get; set; }

    /// <summary>
    /// Позиции счёта
    /// </summary>
    public BillingGatewayPaymentPositionDto[] Positions { get; set; }

    /// <summary>
    /// Признак разового платежа
    /// </summary>
    public bool IsOneTimePayment { get; set; }

    /// <summary>
    /// Продукты, в рамках которых предоставлен доступ к сервису
    /// </summary>
    public string[] ServiceProducts { get; set; }

    /// <summary>
    /// Все продукты, услуги которых предоставляются в платеже
    /// </summary>
    public string[] PositionProducts { get; set; }

    /// <summary>
    /// Флаг наличия доступа к сервису в платеже
    /// </summary>
    public bool HasAccessToService { get; set; }

    /// <summary>
    /// Идентификатор прайс-листа платежа
    /// </summary>
    public int PriceListId { get; set; }

    /// <summary>
    /// Наименование тарифа платежа
    /// </summary>
    public string TariffName { get; set; }

    /// <summary>
    /// Признак платежа сформированного через Новый Биллинг
    /// </summary>
    public bool IsBackofficeBillingPayment { get; set; }

    /// <summary>
    /// Дата начала срока действия платежа
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Дата окончания срока действия платежа
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Наименование прайс-листа
    /// </summary>
    public string PriceListName { get; set; }

    /// <summary>
    /// Список ПУ в платеже. На данный момент не рекомендуется к использованию
    /// </summary>
    public List<string> ProductConfigurations { get; set; }
}
