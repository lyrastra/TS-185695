using System;
using System.Collections.Generic;
using Moedelo.Payroll.Shared.Enums.WorkSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkScheduleHistoryDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WorkScheduleType Type { get; set; }
    public short OnShift { get; set; }
    public short OffShift { get; set; }
    public bool IsPaidHolidays { get; set; }
    public IReadOnlyCollection<WorkPeriodDto> Periods { get; set; }
    public int Id { get; set; }
}