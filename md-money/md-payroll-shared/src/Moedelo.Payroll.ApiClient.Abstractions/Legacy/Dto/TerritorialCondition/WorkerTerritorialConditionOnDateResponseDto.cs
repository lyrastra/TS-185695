using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.TerritorialCondition;

public class WorkerTerritorialConditionOnDateResponseDto
{
    public int WorkerId { get; set; }
    public IReadOnlyCollection<TerritorialConditionOnDateDto> TerritorialConditions { get; set; }
}

public class TerritorialConditionOnDateDto
{
    public DateTime Date { get; set; }

    public TerritorialConditionType TerritorialConditionType { get; set; }
}