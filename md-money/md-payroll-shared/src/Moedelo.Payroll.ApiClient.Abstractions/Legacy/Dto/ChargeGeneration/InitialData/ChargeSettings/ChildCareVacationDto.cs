using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class ChildCareVacationDto
{
    public long SpecialScheduleId { get; set; }
    public DateTime Birthday { get; set; }
    public bool IsFirstChild { get; set; }
    public DateTime? CompensationStartDate { get; set; }
    public DateTime EndDate { get; set; }
}