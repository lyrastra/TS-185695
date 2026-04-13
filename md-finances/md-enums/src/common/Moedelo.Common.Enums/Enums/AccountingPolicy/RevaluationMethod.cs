namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ переоценки.
    /// </summary>
    public enum RevaluationMethod
    {

        /// <summary>
        /// Нет, основные средства не будут переоцениваться.
        /// </summary>
        No = 1,

        /// <summary>
        /// Будут переоцениваться отдельные группы.
        /// </summary>
        RevaluationOfOnlySomeGroups = 2,

        /// <summary>
        /// Да, основные средства будут переоцениваться.
        /// </summary>
        Yes = 3
    }
}