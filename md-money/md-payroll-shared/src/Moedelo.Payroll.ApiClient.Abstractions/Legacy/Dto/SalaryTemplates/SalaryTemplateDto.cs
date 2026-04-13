using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryTemplates;

public class SalaryTemplateDto
{
    public int WorkerId { get; set; }
    public decimal Sum { get; set; }
    public SalaryPayType PayType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}