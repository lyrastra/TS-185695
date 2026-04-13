namespace Moedelo.Requisites.Enums.NdsRatePeriods;

/// <summary>
/// Тип ставки НДС (для УСН)
/// </summary>
/// <remarks>
/// Значения совпадают с enum NdsTypes (однако тут их меньше)
/// </remarks>
public enum NdsRateType
{
    None = -1,
    Nds5 = 5,
    Nds7 = 7,
    Nds20 = 20,
    Nds22 = 22
}