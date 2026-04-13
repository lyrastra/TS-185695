using System;
using Moedelo.Payroll.Shared.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkTypePeriodDto
{
    public WorkType WorkType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}