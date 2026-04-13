namespace Moedelo.Finances.Domain.Enums.Money.Table
{
    public enum MoneyTableExtraBudgetaryFilterType
    {
        None = 0,
        /// <summary> Страховые взносы за сотрудников </summary>
        AllInsuranceFee = 1,
        /// <summary> Фиксированные взносы ИП </summary>
        AllInsuranceFeeIP = 2,
    }
}