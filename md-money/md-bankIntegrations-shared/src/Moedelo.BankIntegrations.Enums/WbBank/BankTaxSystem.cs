namespace Moedelo.BankIntegrations.Enums.WbBank;

/// <summary>
/// Налоговые системы от банка
/// </summary>
public static class BankTaxSystem
{
    /// <summary>
    /// Общая система налогообложения
    /// </summary>
    public const string Osno = "osno";

    /// <summary>
    /// Упрощенная система налогообложения 6% (доходы)
    /// </summary>
    public const string Usn6 = "usn_6";

    /// <summary>
    /// Упрощенная система налогообложения 15% (доходы минус расходы)
    /// </summary>
    public const string Usn15 = "usn_15";

    /// <summary>
    /// Патентная система налогообложения
    /// </summary>
    public const string Psn = "psn";
}
