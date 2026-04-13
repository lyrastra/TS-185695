namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Порядок ведения книги покупок, книги продаж, дополнительных листов к ним,
    /// журнала учета полученных и выданных счетов-фактур.
    /// </summary>
    public enum MethodOfLoggingToBookOfPurchasesAndOthes
    {
        /// <summary>
        /// В электронном виде.
        /// </summary>
        InElectronicForm = 1,

        /// <summary>
        /// На бумажном носителе.
        /// </summary>
        OnPaper = 2
    }
}