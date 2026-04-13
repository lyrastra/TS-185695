using System;
using Moedelo.Payroll.Shared.Enums.RegionRate;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class FirmRegionRateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Rate { get; set; }
    public int? DivisionId { get; set; }
    public RegionRateType Type { get; set; }
}