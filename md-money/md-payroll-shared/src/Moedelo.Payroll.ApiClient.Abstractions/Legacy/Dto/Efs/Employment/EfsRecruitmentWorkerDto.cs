using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.Worker;
using Moedelo.Payroll.Shared.Enums.WorkSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;

public class EfsRecruitmentWorkerDto : EfsBaseWorkerEventDto
{
    public int? PositionId { get; set; }

    public int? PositionHistoryId { get; set; }

    public string PositionName { get; set; }

    public string DivisionName { get; set; }

    public ExecutionFunctionCodeDto ExecutionFunctionCode { get; set; }

    public bool IsPartTime { get; set; }

    public WorkerContractType? WorkerContractType { get; set; }

    public WorkType? WorkType { get; set; }

    public WorkSchedulePartialType? WorkSchedulePartialType { get; set; }

    public DateTime DateOfStartWork { get; set; }
}