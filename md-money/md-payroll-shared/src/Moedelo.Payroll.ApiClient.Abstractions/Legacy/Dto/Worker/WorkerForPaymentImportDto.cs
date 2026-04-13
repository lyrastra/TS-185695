using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerForPaymentImportDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Inn { get; set; }

        public DateTime? DateOfStartWork { get; set; }

        public DateTime? TerminationDate { get; set; }

        public int? WorkerStatus { get; set; }

        public bool IsNotStaff { get; set; }
    }
}