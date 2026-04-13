namespace Moedelo.Money.Api.Models.Registry.OperationTypeSumByPeriod;

/// <summary>
/// Агрегированная модель суммы комисии эквайринга за период
/// </summary>
public class AcquiringCommissionSumByPeriodDto
{
    /// <summary>
    /// Период
    /// </summary>
    public MonthPeriodDto Period { get; set; }

    /// <summary>
    /// Сумма за период
    /// </summary>
    public decimal Sum { get; set; }
}
