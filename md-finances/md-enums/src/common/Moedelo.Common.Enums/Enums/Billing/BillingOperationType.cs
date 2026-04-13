namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum BillingOperationType
    {
        /// <summary>
        /// Подписка/покупка
        /// </summary>
        Buy = 0,

        /// <summary>
        /// Продление подписки
        /// </summary>
        Prolong = 1,

        /// <summary>
        /// Изменение тарифа
        /// </summary>
        ChangeTariff = 2,

        /// <summary>
        /// Допродажа
        /// </summary>
        CrossSelling = 3,
    }
}