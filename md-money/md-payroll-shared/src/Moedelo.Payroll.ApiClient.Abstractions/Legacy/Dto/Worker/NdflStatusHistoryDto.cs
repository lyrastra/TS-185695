using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class NdflStatusHistoryDto
    {
        public int WorkerId { get; set; }

        public WorkerNdflStatus Status { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }
}
