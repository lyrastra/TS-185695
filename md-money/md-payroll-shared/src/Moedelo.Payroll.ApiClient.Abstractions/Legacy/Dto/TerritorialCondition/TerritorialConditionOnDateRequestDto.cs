using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.TerritorialCondition;

public class TerritorialConditionOnDateRequestDto
{
    public int WorkerId { get; set; }
    public DateTime Date { get; set; }
}