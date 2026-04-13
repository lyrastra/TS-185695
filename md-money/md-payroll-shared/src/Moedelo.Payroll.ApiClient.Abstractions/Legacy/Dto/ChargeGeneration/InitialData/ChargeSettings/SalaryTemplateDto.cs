using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class SalaryTemplateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Sum { get; set; }
    public SalaryPayType PayType { get; set; }
    public long Id { get; set; }
}