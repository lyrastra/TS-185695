using System;
using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.PaidConfigurations.Dto;

public class PaidConfigurationResponseDto
{
    /// <summary>
    /// Идентификатор основного счёта в НБ
    /// </summary>
    public int PrimaryBillId { get; set; }

    /// <summary>
    /// Номер счёта
    /// </summary>
    public string BillNumber { get; set; }

    /// <summary>
    /// Дата выставления счёта
    /// </summary>
    public DateTime BillDate { get; set; }

    /// <summary>
    /// Технический код ПУ
    /// </summary>
    public string ProductConfigurationCode { get; set; }

    /// <summary>
    /// Код продукта
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// Начало срока действия ПУ
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Окончание срока действия ПУ
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Срок действия ПУ в месяцах
    /// </summary>
    public int DurationInMonths { get; set; }

    /// <summary>
    /// Список наименований позиций данного счёта, выставленных по продуктовой услуге на данный период
    /// </summary>
    public IReadOnlyCollection<string> PositionNames { get; set; }
}