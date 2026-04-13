using System;

namespace Moedelo.Billing.Abstractions.LimitExcessData.Dto;

public class FirmPeriodRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Начало рассматриваемого периода
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Окончание рассматриваемого периода
    /// </summary>
    public DateTime EndDate { get; set; }
}