using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;
using Moedelo.Payroll.Shared.Enums.WorkerContractSetting;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment
{
    public class EfsWorkContractDto : EfsBaseWorkerEventDto
    {
        public long WorkerContractId { get; set; }
        public WorkerContractType? WorkerContractType { get; set; }
        public DateTime WorkerContractStartDate { get; set; }
        public DateTime WorkerContractEndDate { get; set; }
        public ExecutionFunctionCodeDto ExecutionFunctionCode { get; set; }
    }
}