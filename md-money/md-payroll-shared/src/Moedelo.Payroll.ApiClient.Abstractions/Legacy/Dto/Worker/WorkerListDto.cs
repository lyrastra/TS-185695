using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerListDto
    {
        public List<WorkerDto> Workers { get; set; } = new List<WorkerDto>();
    }
}
