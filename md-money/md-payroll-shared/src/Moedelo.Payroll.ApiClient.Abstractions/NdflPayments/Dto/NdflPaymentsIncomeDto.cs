using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.Enums.Ndfl;

namespace Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

public class NdflPaymentsIncomeDto
{
    public int WorkerId { get; set; }
    public NdflRateType NdflRate { get; set; }
    public int Code { get; set; }
    public decimal Sum { get; set; }
    public DateTime Date { get; set; }
    public int TaxSum { get; set; }
    public DateTime TaxDate { get; set; }
    public IReadOnlyCollection<NdflPaymentsIncomeResidueDto> Residues { get; set; } =
        Array.Empty<NdflPaymentsIncomeResidueDto>();
    public decimal ResidueSum => Residues.Sum(x => x.Sum);
    public int KbkType { get; set; }
}