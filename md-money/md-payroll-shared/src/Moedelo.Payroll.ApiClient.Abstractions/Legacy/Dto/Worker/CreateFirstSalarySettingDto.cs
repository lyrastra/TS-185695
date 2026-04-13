using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class CreateFirstSalarySettingDto
{
    public int WorkerId { get; set; }
    public DateTime WorkStartDate { get; set; }
    public decimal SalarySize { get; set; }
    public SalaryPayType SalaryPayType { get; set; }
}