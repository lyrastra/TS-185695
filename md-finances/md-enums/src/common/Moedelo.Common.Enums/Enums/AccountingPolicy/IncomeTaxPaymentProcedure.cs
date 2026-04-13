namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Порядок уплаты налога на прибыль
    /// </summary>
    public enum IncomeTaxPaymentProcedure
    {
        /// <summary>
        /// Ежеквартально исходя из прибыли, полученной в отчетном квартале
        /// </summary>
        ByQuarter = 1,

        /// <summary>
        /// Ежемесячно исходя из прибыли, полученной в предыдущем квартале
        /// </summary>
        ByMonth = 2,
    }
}