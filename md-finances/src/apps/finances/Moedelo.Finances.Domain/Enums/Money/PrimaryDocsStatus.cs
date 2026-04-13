namespace Moedelo.Finances.Domain.Enums.Money
{
    public enum PrimaryDocsStatus
    {
        /// <summary>
        ///  Не может иметь никаких связанных документов
        /// </summary>
        CantHaveAnyDocs = 0,
        /// <summary>
        ///  Не может иметь первичных документов
        /// </summary>
        CantHavePrimaryDocs = 1,
        /// <summary>
        ///  Не оплачено/не полностью оплачено
        /// </summary>
        UnpaidPrimaryDocs = 2,
        /// <summary>
        ///  Полностью оплачено
        /// </summary>
        PaidPrimaryDocs = 3
    }
}