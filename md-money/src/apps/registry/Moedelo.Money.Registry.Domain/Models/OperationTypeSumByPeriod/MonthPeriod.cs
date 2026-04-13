namespace Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

/// <summary>
/// Модель месячного периода
/// </summary>
public class MonthPeriod
{
    public MonthPeriod(int year, int month)
    {
        Year = year;
        Month = month;
    }

    /// <summary>
    /// Год
    /// </summary>
    public int Year { get; internal set; }

    /// <summary>
    /// Номер месяца в году
    /// </summary>
    public int Month { get; internal set; }
}