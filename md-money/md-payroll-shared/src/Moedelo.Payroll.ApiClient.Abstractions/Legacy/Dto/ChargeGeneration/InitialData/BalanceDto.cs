using System;
using System.Collections.Generic;
using Moedelo.Payroll.Shared.Enums.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class BalanceDto
{
    public DateTime Date { get; set; }
    public IReadOnlyCollection<WorkerBalanceDto> WorkerBalances { get; set; } = [];
}

public class WorkerBalanceDto
{
    public decimal Balance { get; set; }
    public SyntheticAccountCode AccountCode { get; set; }
    public int WorkerId { get; set; }
}