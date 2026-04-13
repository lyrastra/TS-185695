using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkerBonusDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal RegionRate { get; set; }
    public decimal? CustomRegionRate { get; set; }
    public decimal NorthRise { get; set; }
}