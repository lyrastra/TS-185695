using System;
using Moedelo.Payroll.Shared.Enums.WorkSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkPeriodDto
{
    public WorkPeriodType Type { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}