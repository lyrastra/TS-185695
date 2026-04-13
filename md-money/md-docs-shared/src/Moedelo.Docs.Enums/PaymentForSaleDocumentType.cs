namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Входящий рублевый платеж (за документ в Продажах)
    /// </summary>
    public enum PaymentForSaleDocumentType
    {
        /// <summary>
        /// Оплата от покупателя в банке
        /// </summary>
        Bank = 1,

        /// <summary>
        /// Оплата от покупателя в кассе
        /// </summary>
        Cash = 2,
        
        /// <summary>
        /// Оплата от покупателя в эл. деньгах
        /// </summary>
        Purse = 3
    }
}