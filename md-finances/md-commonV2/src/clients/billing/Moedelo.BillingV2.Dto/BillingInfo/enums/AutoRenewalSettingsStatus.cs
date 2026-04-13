namespace Moedelo.BillingV2.Dto.BillingInfo.enums
{
    public enum AutoRenewalSettingsStatus
    {
        /// <summary>
        /// Настройки автопродления созданы
        /// </summary>
        Created = 1,

        /// <summary>
        /// Автопродление включено
        /// </summary>
        Active = 2,

        /// <summary>
        /// Автопродление отключено
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Изменяется метод оплаты для Автопродления
        /// </summary>
        PaymentMethodChanging = 4,

        /// <summary>
        /// Не удалось изменить метод оплаты для Автопродления
        /// </summary>
        PaymentMethodNotChanged = 5
    }
}
