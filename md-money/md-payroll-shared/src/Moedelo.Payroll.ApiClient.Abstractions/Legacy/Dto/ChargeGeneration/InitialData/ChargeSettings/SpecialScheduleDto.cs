using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class SpecialScheduleDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SpecialScheduleTypeDto Type { get; set; }
    public decimal Sum { get; set; }
    public decimal? BonusSum { get; set; }
}