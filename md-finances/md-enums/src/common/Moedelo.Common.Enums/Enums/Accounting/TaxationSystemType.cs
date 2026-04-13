namespace Moedelo.Common.Enums.Enums.Accounting
{
    /// <summary>
    /// Система налогообложения УСН/ОСНО.
    /// </summary>
    public enum TaxationSystemType
    {
        Default = 0,

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
        Patent = 6,

        /// <summary>
        /// УСН и патент
        /// </summary>
        UsnAndPatent = 7,

        /// <summary>
        /// Усн + ЕНВД + Патент
        /// </summary>
        UsnAndPatentAndEnvd = 8,

        /// <summary>
        /// ЕНВД + Патент
        /// </summary>
        EnvdAndPatent = 9
    }
}
