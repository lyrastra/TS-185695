namespace Moedelo.AccountingStatements.Enums
{
    /// <summary>
    /// Система налогообложения
    /// </summary>
    public enum TaxationSystemType
    {
        /// <summary>
        /// УСН
        /// </summary>
        Usn = 1,

        /// <summary>
        /// ОСНО
        /// </summary>
        Osno = 2,

        /// <summary>
        /// ЕНВД
        /// </summary>
        Envd = 3,

        /// <summary>
        /// УСН + ЕНВД
        /// </summary>
        UsnAndEnvd = 4,

        /// <summary>
        /// ОСНО + ЕНВД
        /// </summary>
        OsnoAndEnvd = 5,

        /// <summary>
        /// Патент
        /// </summary>
        Patent = 6
    }
}
