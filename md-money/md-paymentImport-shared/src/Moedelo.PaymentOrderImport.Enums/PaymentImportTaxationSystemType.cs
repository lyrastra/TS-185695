using System.ComponentModel;

namespace Moedelo.PaymentOrderImport.Enums
{
    public enum PaymentImportTaxationSystemType
    {
        /// <summary>
        /// УСН
        /// </summary>
        [Description("УСН")]
        Usn = 1,

        /// <summary>
        /// ОСНО
        /// </summary>
        [Description("ОСНО")]
        Osno = 2,

        /// <summary>
        /// ЕНВД
        /// </summary>
        [Description("ЕНВД")]
        Envd = 3,

        /// <summary>
        /// УСН + ЕНВД
        /// </summary>
        [Description("УСН + ЕНВД")]
        UsnAndEnvd = 4,

        /// <summary>
        /// ОСНО + ЕНВД
        /// </summary>
        [Description("ОСНО + ЕНВД")]
        OsnoAndEnvd = 5,

        /// <summary>
        /// Патент
        /// </summary>
        [Description("Патент")]
        Patent = 6
    }
}
