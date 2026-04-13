namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ учета управленченских расходов.
    /// </summary>
    public enum AccountingMethodForAdministrativeCosts
    {
        /// <summary>
        /// Учитывать на счете 26 «Общехозяйственные расходы» в качестве условно-постоянных расходов.
        /// </summary>
        OnAccountNumber26AsFixedCosts = 1,

        /// <summary>
        /// Отражать на счете 20 «Основное производство» (44 «Издержки обращения») без 
        /// использования счета 26 «Общехозяйственные расходы».
        /// </summary>
        OnAccountsNumber20Or44WithoutNumber26 = 2,

        /// <summary>
        /// Учитывать на счете 26 «Общехозяйственные расходы» и списываются ежемесячно 
        /// на счет 20 «Основное производство» (44 «Издержки обращения»).
        /// </summary>
        OnAccountsNumber26AndMonthlyOnNumber20Or40 = 3
    }
}