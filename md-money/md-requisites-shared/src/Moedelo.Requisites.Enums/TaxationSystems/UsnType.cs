namespace Moedelo.Requisites.Enums.TaxationSystems
{
    public enum UsnType
    {
        [DefaultTaxRate(6)]
        Profit = 1,

        [DefaultTaxRate(15)]
        ProfitAndOutgo = 2
    }
}
