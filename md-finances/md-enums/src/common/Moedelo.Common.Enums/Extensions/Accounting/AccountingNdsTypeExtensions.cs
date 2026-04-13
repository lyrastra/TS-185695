using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Extensions.Accounting;

public static class AccountingNdsTypeExtensions
{
    /// <summary>
    /// Возвращает значение коэффициента для ставки НДС
    /// </summary>
    public static decimal GetNdsRatio(this AccountingNdsType type)
    {
        return type switch
        {
            AccountingNdsType.None or AccountingNdsType.Nds0 => 0m,
            AccountingNdsType.Nds10 or AccountingNdsType.Nds10To110 => 10m / 110m,
            AccountingNdsType.Nds18 or AccountingNdsType.Nds18To118 => 18m / 118m,
            AccountingNdsType.Nds20 or AccountingNdsType.Nds20To120 => 20m / 120m,
            AccountingNdsType.Nds5 or AccountingNdsType.Nds5To105 => 5m / 105m,
            AccountingNdsType.Nds7 or AccountingNdsType.Nds7To107 => 7m / 107m,
            AccountingNdsType.Nds22 or AccountingNdsType.Nds22To122 => 22m / 122m,
            _ => 0m
        };
    }
}