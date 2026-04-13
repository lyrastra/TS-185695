using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class ForeignerStatusHistoryDto
    {
        public int WorkerId { get; set; }

        public WorkerForeignerStatus ForeignerStatus { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public bool? ByPatent { get; set; }
    }
}
