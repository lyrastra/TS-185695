using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareAllowanceIdRequestDto
    {
        public WorkerRequestDto Worker { get; set; }
        
        public DateTime? BirthDate { get; set; }
    }
}