namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Периодичность формирования промежуточной отчётности.
    /// </summary>
    public enum PeriodicityOfIntermReporting
    {
        /// <summary>
        /// Ежемесячно.
        /// </summary>
        Monthly = 1,

        /// <summary>
        /// Ежеквартально.
        /// </summary>
        Quarterly = 2,

        /// <summary>
        /// Никогда не формировать.
        /// </summary>
        Never = 3
    }
}