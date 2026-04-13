using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Enums.SalarySettings;
using Moedelo.Payroll.Shared.Enums.Worker;
using Moedelo.Payroll.Shared.Enums.WorkSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment
{
    public class EfsPositionChangesDto
    {
        public int WorkerId { get; set; }

        public TerritorialConditionType TerritorialCondition { get; set; }
        public int PositionId { get; set; }

        public int PositionHistoryId { get; set; }

        public string PositionName { get; set; }

        public string DivisionName { get; set; }

        public ExecutionFunctionCodeDto ExecutionFunctionCode { get; set; }

        public bool IsPartTime { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WorkerContractType? WorkerContractType { get; set; }

        [Obsolete]
        public WorkType? WorkType { get; set; }

        [Obsolete]
        public WorkSchedulePartialType? WorkSchedulePartialType { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}