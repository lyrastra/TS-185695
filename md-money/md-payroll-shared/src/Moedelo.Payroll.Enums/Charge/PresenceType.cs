namespace Moedelo.Payroll.Shared.Enums.Charge;

public enum PresenceType
{
    /// <summary> Оплата командировки по среднему заработку </summary>
    BusinessTrip = 5001,
    
    /// <summary>
    /// Командировочные расходы
    /// </summary>
    BusinessTripExpenses = 5021,

    /// <summary>
    /// Сверхнормативные суточные
    /// </summary>
    BusinessTripOverDailyAllowances = 5022,

    /// <summary>
    /// Аванс командировочных расходов
    /// </summary>
    BusinessTripAdvanceExpenses = 5023
}