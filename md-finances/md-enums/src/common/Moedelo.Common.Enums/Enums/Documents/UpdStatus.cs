namespace Moedelo.Common.Enums.Enums.Documents
{
    /// <summary>
    /// Статус УПД
    /// </summary>
    public enum UpdStatus
    {
        /// <summary>
        /// Одновременно счета-фактура и первичный документ
        /// </summary>
        DocumentAndInvoice = 1,

        /// <summary>
        /// Только первичный документ
        /// </summary>
        Document = 2
    }
}