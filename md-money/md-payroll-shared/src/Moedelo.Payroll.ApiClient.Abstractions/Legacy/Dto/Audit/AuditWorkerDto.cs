using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit
{
    public class AuditWorkerDto
    {
        public int Id { get; set; }

        public string Fio { get; set; }

        public FounderType FounderType { get; set; }

        public DateTime? DateOfStartWork { get; set; }
        
        public DateTime? TerminationDate { get; set; }
        
        public bool IsStaffWorker { get; set; }
    }
}
