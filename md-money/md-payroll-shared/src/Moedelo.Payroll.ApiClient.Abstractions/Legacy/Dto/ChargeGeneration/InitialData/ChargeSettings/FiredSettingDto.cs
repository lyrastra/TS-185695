using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class FiredSettingDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Indexation { get; set; }
    public decimal Sum { get; set; }
    public decimal? BonusSum { get; set; }
    public decimal? TaxFreeSum { get; set; }
    public decimal? BonusTaxFreeSum { get; set; }
    public int ChargeTypeId { get; set; }
    public decimal? NdflTaxFreeSum { get; set; }
    public decimal? BonusNdflTaxFreeSum { get; set; }
}