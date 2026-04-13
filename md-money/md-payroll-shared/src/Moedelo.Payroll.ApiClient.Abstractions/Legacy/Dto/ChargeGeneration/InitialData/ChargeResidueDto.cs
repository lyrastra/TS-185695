using Moedelo.Payroll.Shared.Enums.Residues;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeResidueDto
{
    public long Id { get; set; }
    public ResiduesNdflCode Code { get; set; }
    public decimal Sum { get; set; }
    public int Year { get; set; }
}