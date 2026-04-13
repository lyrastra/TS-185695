namespace Moedelo.Money.Domain.LinkedDocuments
{
    /// <summary>
    /// todo: Если будут появляться типы, не нужные для операций с документами, выделить отдельный enum
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,

        /// <summary>
        /// Счет-фактура
        /// </summary>
        Invoice = 2,

        /// <summary>
        /// Акт
        /// </summary>
        Statement = 6,

        /// <summary>
        /// Входящий универсальный передаточный документ
        /// </summary>
        Upd = 33,

        /// <summary>
        /// Исходящий универсальный передаточный документ
        /// </summary>
        SalesUpd = 36,
        
        /// <summary>
        /// Исходящий инвойс
        /// </summary>
        SalesCurrencyInvoice = 55
    }
}
