namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum PromoCodeOfferType : byte
    {
        /// <summary>
        /// Не указан тип промокода
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Сумма оплаты меняется на указанную в поле промокода
        /// </summary>
        FixedSum = 1,

        /// <summary>
        /// Сумма оплаты уменьшается на процент, указанный в поле промокода
        /// </summary>
        PercentRate = 2,

        /// <summary>
        /// Дополнительные месяцы к дате окончания оплаты, указанные в промокоде
        /// </summary>
        MonthesAsBonus = 3,

        /// <summary>
        /// Дата окончания платежа становится такой же, как в поле промокода
        /// </summary>
        ExpirationDateAsBonus = 4,

        /// <summary>
        /// Сумма оплаты уменьшается на сумму, указанную в поле промокода
        /// </summary>
        DiscountFixedSum = 5,

        /// <summary>
        /// Сумма оплаты уменьшается по накопительной системе с шагом "сумма, указанная в поле промокода"
        /// </summary>
        DiscountAccumulateSum = 6
    }
}