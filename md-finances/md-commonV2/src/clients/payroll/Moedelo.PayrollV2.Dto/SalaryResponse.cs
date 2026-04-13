using Moedelo.PayrollV2.Dto.Employees;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto
{
    public class SalaryResponse
    {
        public List<WorkerMonthlySalaryDto> Salary { get; set; }
    }
}
