using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
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