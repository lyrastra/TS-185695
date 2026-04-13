namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Исходящий рублевый платеж (за документ в Покупках)
    /// </summary>
    public enum PaymentForPurchaseDocumentType
    {
        /// <summary>
        /// Оплата поставщику в банке
        /// </summary>
        Bank = 1,

        /// <summary>
        /// Оплата поставщику в кассе
        /// </summary>
        Cash = 2,
        
        /// <summary>
        /// Авансовый отчет с типом "Оплата поставщику"
        /// </summary>
        Ao = 3
    }
}