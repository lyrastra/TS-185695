using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerScheduleHistoryDto
    {
        public int WorkerId { get; set; }

        public decimal WorkRate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }
}
