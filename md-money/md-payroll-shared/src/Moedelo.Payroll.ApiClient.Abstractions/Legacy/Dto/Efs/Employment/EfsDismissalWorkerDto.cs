using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;

public class EfsDismissalWorkerDto : EfsBaseWorkerEventDto
{
    public long? FiredWorkerId { get; set; }

    public string FullFiredReason { get; set; }

    public string FiredCode { get; set; }

    public string ShortFiredReason { get; set; }

    public int? PositionId { get; set; }

    public int? PositionHistoryId { get; set; }

    public string PositionName { get; set; }

    public string DivisionName { get; set; }

    public ExecutionFunctionCodeDto ExecutionFunctionCode { get; set; }

    public bool IsPartTime { get; set; }

    public WorkerContractType? WorkerContractType { get; set; }

    public DateTime TerminationDate { get; set; }
}