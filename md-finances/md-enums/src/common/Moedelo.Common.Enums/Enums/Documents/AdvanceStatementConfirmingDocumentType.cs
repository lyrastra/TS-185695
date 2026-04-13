namespace Moedelo.Common.Enums.Enums.Documents
{
    /// <summary>
    /// Типы подтверждающих документов в авансовом отчете
    /// </summary>
    public enum AdvanceStatementConfirmingDocumentType
    {
        /// <summary>
        /// Акт из покупок
        /// </summary>
        Statement = 2,
        
        /// <summary>
        /// Накладная из покупок
        /// </summary>
        Waybill = 3,
        
        /// <summary>
        /// Универсальный передаточный документ
        /// </summary>
        Upd = 4,

        /// <summary>
        /// Псевдоакт для биза с отключенным складом
        /// </summary>
        StocklessStatement = 22,

        /// <summary>
        /// Псевдонакладная по товарам для биза с отключенным складом
        /// </summary>
        StocklessProductWaybill = 23,

        /// <summary>
        /// Псевдонакладная по материалам для биза с отключенным складом
        /// </summary>
        StocklessMaterialWaybill = 24
    }
}
