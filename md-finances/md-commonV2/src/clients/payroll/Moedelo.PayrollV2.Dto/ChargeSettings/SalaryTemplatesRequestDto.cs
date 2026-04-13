using System;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.ChargeSettings
{
    public class SalaryTemplatesRequestDto
    {
        public SalaryTemplatesRequestDto()
        {
            workerIds = new List<int>();
        }
        public List<int> workerIds { get; set; }
        public DateTime? onDate { get; set; }
    }
}