namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ принятия к учету целевых средств.
    /// </summary>
    public enum AccountingMethodForEarmarkedFunds
    {
        /// <summary>
        /// По мере фактического получения целевых средств.
        /// </summary>
        AsActualReceiptOfEarmarkedFfunds = 1,

        /// <summary>
        /// По мере принятия решения о предоставлении бюджетных средств.
        /// </summary>
        AsDecisionToGrantBudget = 2
    }
}