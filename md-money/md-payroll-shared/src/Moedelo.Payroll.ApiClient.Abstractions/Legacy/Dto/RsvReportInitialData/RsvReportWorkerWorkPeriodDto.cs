using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData
{
    public class RsvReportWorkerWorkPeriodDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int WorkerId { get; set; }
    }
}