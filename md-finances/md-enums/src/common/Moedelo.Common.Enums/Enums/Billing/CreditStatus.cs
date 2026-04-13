namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum CreditStatus
    {
        /// <summary>
        /// Пользователь прочитал и согласился с условиями, и тем самым создал заявку. 
        /// </summary>
        AgreedWithTermsAndCondition = 10,

        /// <summary>
        /// Заявка отправлена в банк. Этот статус висит до того момента, как заявку одобрят в банке и пришлют нам уведомление. 
        /// </summary>
        SendedToBank = 20,

        /// <summary>
        /// Заявку подтвердил банк - можно сказать пользователю, что у него всё ништяк. 
        /// </summary>
        ApprovedByBank = 30,

        /// <summary>
        /// Заявку подтвердил наш менеджер и позвонил пользователю. 
        /// </summary>
        ApprovedByManager = 35,

        /// <summary>
        /// Пользователь скачал заявление-анкету. 
        /// </summary>
        ContractDownloaded = 40,

        /// <summary>
        /// Пользователь должен дождаться курьера. 
        /// </summary>
        WaitingCourier = 45,

        /// <summary>
        /// Пользователь знает, что куда отправлять или ожидает курьера и нажал кнопку "Завершить". 
        /// </summary>
        Finished = 50,

        /// <summary>
        /// Пользователь отменил заявку. 
        /// </summary>
        /// <remarks>
        /// Значение ДОЛЖНО БЫТЬ выше, чем Finished. 
        /// </remarks>
        Cancelled = 51,

        /// <summary>
        /// Заявку отклонили. 
        /// </summary>
        /// <remarks>
        /// Значение ДОЛЖНО БЫТЬ выше, чем Finished. 
        /// </remarks>
        Rejected = 52,

        /// <summary>
        /// Документы по заявке пришли в офис и ее подтвердили. 
        /// </summary>
        /// <remarks>
        /// Значение ДОЛЖНО БЫТЬ выше, чем Finished. 
        /// </remarks>
        Commited = 55
    }
}
