using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public class VacationsRequestDto
    {
        public VacationsRequestDto()
        {
            workerIds = new List<int>();
        }
        public List<int> workerIds { get; set; }
        
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}