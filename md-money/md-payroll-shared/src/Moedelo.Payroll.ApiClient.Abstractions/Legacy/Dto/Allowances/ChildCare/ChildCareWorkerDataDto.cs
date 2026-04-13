using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareWorkerDataDto
    {
        public int WorkerId { get; set; }
        public string Snils { get; set; }
        public List<WorkerCalculationIllnessYearDto> Years { get; set; }
    }
}