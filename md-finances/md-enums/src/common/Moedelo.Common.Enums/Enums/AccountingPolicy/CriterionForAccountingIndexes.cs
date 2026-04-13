namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Критерий существенности для отражения обособленно
    /// показателей об отдельных активах, обязательствах, доходах, расходах и
    /// хозяйственных операциях в бухгалтерской (финансовой) отчетности.
    /// </summary>
    public enum CriterionForAccountingIndexes
    {
        /// <summary>
        /// 5% от валюты баланса.
        /// </summary>
        ByFivePercentOfTotalBalanceCriterion = 1,

        /// <summary>
        /// По иному показателю.
        /// </summary>
        ByAnotherCriterion = 2
    }
}