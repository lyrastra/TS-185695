namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Оценка товаров для розничной торговли.
    /// </summary>
    public enum EstimateMethodForProductsCost
    {
        /// <summary>
        /// В покупных ценах.
        /// </summary>
        InPurchasePrices = 1,

        /// <summary>
        /// В продажных ценах с отдельным учетом наценок (скидок) на счете 42 «Торговая наценка».
        /// </summary>
        InSalePricesWithDiscounts = 2
    }
}