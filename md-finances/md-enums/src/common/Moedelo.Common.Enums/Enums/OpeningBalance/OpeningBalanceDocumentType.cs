namespace Moedelo.Common.Enums.Enums.OpeningBalance
{
    /// <summary>
    /// Значения из enum https://gitlab.mdtest.org/development/infra/md-enums/-/blob/master/src/common/Moedelo.Common.Enums/Enums/Documents/AccountingDocumentType.cs
    /// </summary>
    public enum OpeningBalanceDocumentType
    {
        Default = 0,
        
        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,
        
        /// <summary>
        /// Акт
        /// </summary>
        Statement = 6,

        /// <summary>
        /// Неизвестный нам подтверждающий документ
        /// </summary>
        Other = 101
    }
}
