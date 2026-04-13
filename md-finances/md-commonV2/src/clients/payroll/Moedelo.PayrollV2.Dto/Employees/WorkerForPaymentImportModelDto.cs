using System;

namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerForPaymentImportModelDto
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