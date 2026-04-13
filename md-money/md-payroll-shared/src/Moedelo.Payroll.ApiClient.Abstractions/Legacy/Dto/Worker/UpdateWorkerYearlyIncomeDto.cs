using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class UpdateWorkerYearlyIncomeDto
{
    public int WorkerId { get; set; }
    public DateTime WorkStartDate { get; set; }
    public DateTime? OldWorkStartDate { get; set; }
}