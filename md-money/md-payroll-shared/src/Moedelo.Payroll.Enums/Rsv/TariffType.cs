namespace Moedelo.Payroll.Enums.Rsv;

public enum TariffType
{
    /// <summary>
    /// до 2020 года
    /// </summary>
    TypeBefore2020 = 0,
    /// <summary>
    /// тариф 2020 года
    /// </summary>
    Type2020,
    /// <summary>
    /// тариф 2020 года до превышения МРОТ
    /// </summary>
    Type2020Mrot,
    /// <summary>
    /// тариф 2020 года при превышении МРОТ
    /// </summary>
    Type2020OverMrot,
    /// <summary>
    /// тариф 2020 года для пострадавших отраслей 
    /// </summary>
    Type2020TaxAmnesty
}