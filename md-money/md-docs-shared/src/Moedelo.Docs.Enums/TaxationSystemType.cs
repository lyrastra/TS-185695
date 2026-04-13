namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// СНО которые можно выбрать в первичных документах
    /// </summary>
    public enum TaxationSystemType
    {
        /// <summary>
        /// УСНО.
        /// </summary>
        Usn = 1,

        /// <summary>
        /// ОСНО.
        /// </summary>
        Osno = 2,

        /// <summary>
        /// ЕНВД.
        /// </summary>
        Envd = 3,

        /// <summary>
        /// УСНО + ЕНВД.
        /// </summary>
        UsnAndEnvd = 4,

        /// <summary>
        /// ОСНО + ЕНВД.
        /// </summary>
        OsnoAndEnvd = 5,
        
        /// <summary>
        /// Патент
        /// </summary>
        Patent = 6
    }
}