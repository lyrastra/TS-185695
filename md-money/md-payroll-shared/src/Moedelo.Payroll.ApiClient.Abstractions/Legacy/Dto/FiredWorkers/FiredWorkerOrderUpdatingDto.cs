using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FiredWorkers
{
    public class FiredWorkerOrderUpdatingDto
    {
        public long FiredWorkerId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
