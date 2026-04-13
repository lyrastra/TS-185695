using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class WorkerStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public IReadOnlyCollection<WorkerInfoDto> PrevData { get; set; }
    }
}