using System;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class WorkerEmploymentChangeDto
    {
        public EmploymentChangingType Type { get; set; }

        public int WorkerId { get; set; }
        
        public int? PositionId { get; set; }

        public string PositionName { get; set; }

        public string DivisionName { get; set; }

        public DateTime Date { get; set; }

        public int? PositionHistoryId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? FiredWorkerId { get; set; }

        public string AdditionalInfo { get; set; } //поле для доп. информации, например, о причине увольнения

        public ExecutionFunctionCodeDto ExecutionFunctionCode { get; set; }
        
        public TerritorialConditionType TerritorialCondition { get; set; }

        public bool IsPartTime { get; set; }
    }
}
