using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit
{
    public class AuditUnboundPaymentsRequest
    {
        public AuditUnboundPaymentsRequest()
        {
            WorkerIds = new List<int>();
        }
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IReadOnlyCollection<int> WorkerIds { get; set; }
    }
}
