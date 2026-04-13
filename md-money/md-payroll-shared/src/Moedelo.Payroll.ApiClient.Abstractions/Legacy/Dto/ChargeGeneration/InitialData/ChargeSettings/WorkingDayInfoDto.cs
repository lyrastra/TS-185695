using System;
using Moedelo.Payroll.Shared.Enums.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkingDayInfoDto
{
    public DateTime Day { get; set; }
    public DayInfoType DayType { get; set; }
    public long SpecialScheduleId { get; set; }
}