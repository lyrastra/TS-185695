using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class BusinessTripExpensesSettingDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Sum { get; set; }
    public decimal TaxableSum { get; set; }
    public decimal FundChargeSum { get; set; }
    public decimal? OverDailyAllowancesSum { get; set; }
}