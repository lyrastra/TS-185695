namespace Moedelo.Common.Enums.Enums.Crm
{
    public enum CrmTaskType
    {
        /// <summary>
        /// Отклик со страницы управленческого учета
        /// </summary>
        ResponseFromTheManagementAccountingPage,

        /// <summary>
        /// Обновление номера телефона
        /// </summary>
        UpdatePhoneNumber,

        /// <summary>
        /// Отклик с рассылки
        /// </summary>
        ResponseFromMailingList,

        /// <summary>
        /// Заявка из ТДЦ
        /// </summary>
        RequestFromTdc,

        /// <summary>
        /// Заявка на разовую услугу
        /// </summary>
        ApplicationOneTimeService,

        /// <summary>
        /// Переход на ОСНО
        /// </summary>
        TransitionToOsno,

        /// <summary>
        /// Спецпредложения для фримиум
        /// </summary>
        SpecialOffersForFreemium,

        /// <summary>
        /// Закрыть ООО. Аутсорсинг
        /// </summary>
        CloseOooOutsourcing,

        /// <summary>
        /// Управленческий учет
        /// </summary>
        ManagementAccounting,

        /// <summary>
        /// Управленческий учёт, оплаченный пользователь
        /// </summary>
        ManagementAccountingPaidUser,

        /// <summary>
        /// Запись на обучение из ЛК
        /// </summary>
        RegistrationTrainingFromPersonalAccount,
        
        /// <summary>
        /// Заявка на оплату сервиса
        /// </summary>
        PaymentOfService,

        /// <summary>
        /// Заявка на подключение АУТ из мобильного приложения
        /// </summary>
        ApplicationOutFromMobileApplication,

        /// <summary>
        /// Заявка на подключение ИБ из мобильного приложения
        /// </summary>
        ApplicationIBFromMobileApplication,

        /// <summary>
        /// Заявка на подключение УУ из мобильного приложения
        /// </summary>
        ApplicationUUFromMobileApplication,

        /// <summary>
        /// Заявка на подключение разовой услуги из мобильного приложения
        /// </summary>
        ApplicationOneTimeServiceFromMobileApplication,

        /// <summary>
        /// Заявка на подключение АУТ или ИБ из мобильного приложения
        /// </summary>
        ApplicationOutOrIBFromMobileApplication,

        /// <summary>
        /// Заявка на покупку опции Торговля на маркетплейсах с главной страницы
        /// </summary>
        ApplicationOptionTradingOnMarketplacesFromMainPage,
        
        /// <summary>
        /// Заявка на АУТ в связи с превышением лимита дохода с диалогового окна на главной
        /// </summary>
        OutsourceIncomeLimitExceeded,
    }
}