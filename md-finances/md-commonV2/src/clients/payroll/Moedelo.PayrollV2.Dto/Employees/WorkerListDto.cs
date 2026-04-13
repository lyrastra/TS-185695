using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerListDto
    {
        public List<WorkerDto> Workers { get; set; } = new List<WorkerDto>();
    }
}
