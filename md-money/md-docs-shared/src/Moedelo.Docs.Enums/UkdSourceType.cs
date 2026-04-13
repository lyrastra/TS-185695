namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Виды документов коректируемые из УКД
    /// </summary>
    public enum UkdSourceType
    {
        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,
        /// <summary>
        /// Акт
        /// </summary>
        Statement = 6,
        /// <summary>
        /// ОРП
        /// </summary>
        Orp = 12,
        /// <summary>
        /// УПД
        /// </summary>
        Upd = 36,
        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        CommissionAgentReport = 59,
    }
}