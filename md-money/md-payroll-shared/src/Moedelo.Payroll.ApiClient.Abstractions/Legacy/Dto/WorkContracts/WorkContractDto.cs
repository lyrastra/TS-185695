using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts
{
    public class WorkContractDto
    {
        public WorkContractDto()
        {
            WorkerIds = new List<int>();
        }

        public int FirmId { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public IReadOnlyCollection<int> WorkerIds { get; set; }
        
        public DateTime PaymentPeriodEndDate { get; set; }
    }
}