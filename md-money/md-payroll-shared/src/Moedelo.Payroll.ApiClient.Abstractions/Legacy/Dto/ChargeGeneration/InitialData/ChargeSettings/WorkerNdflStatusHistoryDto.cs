using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkerNdflStatusHistoryDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WorkerNdflStatus Status { get; set; }
}