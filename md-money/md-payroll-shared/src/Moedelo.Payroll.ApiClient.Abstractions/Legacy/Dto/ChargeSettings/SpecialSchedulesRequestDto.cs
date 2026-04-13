using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public class SpecialSchedulesRequestDto
    {
        public SpecialSchedulesRequestDto()
        {
            WorkerIds = new List<int>();
            Codes = new List<int>();
        }
        public List<int> WorkerIds { get; set; }

        public List<int> Codes { get; set; }

        public PeriodDto Period { get; set; }
    }
}