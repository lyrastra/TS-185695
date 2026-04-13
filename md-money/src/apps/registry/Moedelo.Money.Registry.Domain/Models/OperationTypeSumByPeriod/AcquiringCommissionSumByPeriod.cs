namespace Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

/// <summary>
/// Агрегированная модель суммы комисии эквайринга за период
/// </summary>
public class AcquiringCommissionSumByPeriod
{
    /// <summary>
    /// Период
    /// </summary>
    public MonthPeriod Period { get; set; }

    /// <summary>
    /// Сумма за период
    /// </summary>
    public decimal Sum { get; set; }
}
