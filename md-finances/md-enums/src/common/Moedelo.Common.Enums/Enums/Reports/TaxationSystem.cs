namespace Moedelo.Common.Enums.Enums.Reports
{
    /// <summary>
    /// Система налогообложения
    /// </summary>
    public enum TaxationSystem
    {
        Unknown = 0,

        /// <summary>
        /// УСН 6
        /// </summary>
        Usn6 = 1,

        /// <summary>
        /// УСН 15
        /// </summary>
        Usn15 = 2,

        /// <summary>
        /// УСН 6 + ЕНВД
        /// </summary>
        Usn6AndEnvd = 3,

        /// <summary>
        /// УСН 15 + ЕНВД
        /// </summary>
        Usn15AndEnvd = 4,

        /// <summary>
        /// ЕНВД
        /// </summary>
        Envd = 5,

        /// <summary>
        /// ОСНО
        /// </summary>
        Osno = 6,

        /// <summary>
        /// ОСНО + ЕНВД
        /// </summary>
        OsnoAndEnvd = 7
    }
}