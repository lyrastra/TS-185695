using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum UkdSourceType
    {
        /// <summary>
        /// Акт
        /// </summary>
        [Description("Акт")]
        Statement = 6,

        /// <summary>
        /// Накладная
        /// </summary>
        [Description("Накладная")]
        Waybill = 1,

        /// <summary>
        /// УКД
        /// </summary>
        [Description("УПД")]
        Upd = 36,

        /// <summary>
        /// Отчет о розничной продаже
        /// </summary>
        [Description("ОРП")]
        Orp = 12,

        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        [Description("Отчет комиссионера")]
        CommissionAgentReport = 59
    }
}
