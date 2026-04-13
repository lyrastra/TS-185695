using System;

namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerMonthlySalaryDto
    {
        public double Summ { get; set; }

        public int WorkerId { get; set; }

        public DateTime PeriodDate { get; set; }
    }
}
