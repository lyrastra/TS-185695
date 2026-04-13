using System;
using Moedelo.Payroll.Shared.Enums.Deduction;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class DeductionDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DeductionSizeType DeductionSizeType { get; set; }
    public decimal DeductionSize { get; set; }
    public bool IsMonthly { get; set; }
}