namespace Moedelo.Common.Enums.Enums.Requisites
{
    /// <summary> Тип УСН. </summary>
    public enum UsnTypes
    {
        Default = 0,

        [DefaultTaxRate(6)]
        Profit = 1,

        [DefaultTaxRate(15)]
        ProfitAndOutgo = 2
    }
}