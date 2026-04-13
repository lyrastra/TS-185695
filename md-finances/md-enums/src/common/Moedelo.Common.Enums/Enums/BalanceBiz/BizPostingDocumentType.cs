namespace Moedelo.Common.Enums.Enums.BalanceBiz
{
    /// <summary>
    /// Тип документа, на основе которого сгенерирована проводка
    /// </summary>
    public enum BizPostingDocumentType
    {
        /// <summary>
        /// Подтверждающая накладная
        /// </summary>
        ConfirmingWaybill = 1,

        /// <summary>
        /// Подтверждающий акт
        /// </summary>
        ConfirmingStatement = 2,

        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 3,

        /// <summary>
        /// Акт
        /// </summary>
        Statement = 4
    }
}
