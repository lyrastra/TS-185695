using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
{
    public class FullWorkerEmploymentChangesDto
    {
        public int WorkerId { get; set; }

        public List<WorkerEmploymentChangeDto> Changes { get; set; }
    }
}
