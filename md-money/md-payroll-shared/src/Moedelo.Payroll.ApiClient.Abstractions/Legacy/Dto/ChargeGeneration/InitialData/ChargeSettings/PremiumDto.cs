using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class PremiumDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal SizePayment { get; set; }
    public decimal PercentSize { get; set; }
    public int ChargeTypeId { get; set; }
    public bool IsRegular { get; set; }
    public bool IsApplyNorthRise { get; set; }
    public bool IsApplyRegionalRate { get; set; }
    public long Id { get; set; }
}