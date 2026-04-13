using System;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventSalarySettingsDto
{
    /// <summary>
    /// Расчетный счет с которого платится зарплата
    /// </summary>
    public int? SalarySettlementAccountId { get; set; }

    /// <summary>
    /// Дата с которой начинается начисление заработной платы
    /// </summary>
    public DateTime SalaryChargingStartDate { get; set; }

    /// <summary>
    /// Тип периода выплаты заработной платы
    /// </summary>
    public SalaryPaymentPeriod SalaryPaymentPeriod { get; set; }
}