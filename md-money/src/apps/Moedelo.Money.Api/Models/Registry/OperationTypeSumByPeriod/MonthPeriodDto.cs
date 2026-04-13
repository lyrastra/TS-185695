namespace Moedelo.Money.Api.Models.Registry.OperationTypeSumByPeriod;

public class MonthPeriodDto
{
    public MonthPeriodDto(int year, int month)
    {
        Year = year;
        Month = month;
    }

    /// <summary>
    /// Год
    /// </summary>
    public int Year { get; private set; }

    /// <summary>
    /// Номер месяца в году
    /// </summary>
    public int Month { get; private set; }
}