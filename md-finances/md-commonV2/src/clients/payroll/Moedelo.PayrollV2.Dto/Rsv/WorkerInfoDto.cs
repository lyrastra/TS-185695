using System.Collections.Generic;
using Moedelo.PayrollV2.Dto.Employees;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class WorkerInfoDto
    {
        public WorkerDto Worker { get; set; } = new WorkerDto();
        public bool IsPfr { get; set; }
        public bool IsFfoms { get; set; }
        public bool IsFss { get; set; }
        public bool IsLowTariff { get; set; }
        public bool IsTerminated { get; set; }
        public List<WorkerPfrDto> PfrTable { get; set; } = new List<WorkerPfrDto>();
        public WorkerLowTariffDto LowTariffTable { get; set; } = new WorkerLowTariffDto();
    }
}