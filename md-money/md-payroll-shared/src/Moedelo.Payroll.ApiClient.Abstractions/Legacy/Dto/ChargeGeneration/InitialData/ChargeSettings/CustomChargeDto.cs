using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class CustomChargeDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Sum { get; set; }
    public bool? IsNewDividends { get; set; }
    public int ChargeTypeId { get; set; }
}