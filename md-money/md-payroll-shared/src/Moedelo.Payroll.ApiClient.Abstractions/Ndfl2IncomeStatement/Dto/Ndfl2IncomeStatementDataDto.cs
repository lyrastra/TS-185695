using System;
using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement.Dto;

public class Ndfl2IncomeStatementDataDto
{
    public Ndfl2IncomeStatementWorkerDto Worker { get; set; } = new();
    public IReadOnlyCollection<NdflPaymentsIncomeDto> Incomes { get; set; } = Array.Empty<NdflPaymentsIncomeDto>();
}