using System;
using Moedelo.Payroll.Shared.Enums.Residues;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class ResidueDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal ResidueSize { get; set; }
    public int ResidueTypeCode { get; set; }
    public bool IsChild { get; set; }
    public ResiduesNdflCode ResidueCode { get; set; }
    public decimal? Sum { get; set; }
    public decimal? MaxIncome { get; set; }
    public long Id { get; set; }
}