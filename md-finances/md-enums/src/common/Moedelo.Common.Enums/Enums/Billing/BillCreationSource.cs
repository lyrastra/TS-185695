namespace Moedelo.Common.Enums.Enums.Billing
{
    public enum BillCreationSource
    {
        Unknown = 0,

        /// <summary>
        /// Создан из бэкофис биллинга оператором
        /// </summary>
        OperatorInterface = 1,

        /// <summary>
        /// Создан самим пользователем с маркетплейса
        /// </summary>
        Marketplace = 2,

        /// <summary>
        /// Создан в процессе автопродления
        /// </summary>
        AutoRenewal = 3,

        /// <summary>
        /// Создан из мобильного приложения
        /// </summary>
        MobileApp = 4,

        /// <summary>
        /// Автоплатеж из банка (из выписки)
        /// </summary>
        BankAutoPayment = 5,

        /// <summary>
        /// Трансфер БИП юзеров Сбера
        /// </summary>
        TransferSberbankBIP = 6,
        
        /// <summary>
        /// Регистрация клиентов через sso
        /// </summary>
        RegistrationMts = 7,

        /// <summary>
        /// Автоплатеж из банка (по подписки)
        /// </summary>
        BankSubscription = 8,
        
        /// <summary>
        /// Продажи с лендинга - новая схема для банк партнеров
        /// </summary>
        SalesOnLandingPage = 9,
        
        /// <summary>
        /// Голосовой ассистент 
        /// </summary>
        VoiceAssistant = 10,

        /// <summary> Автовыставление счетов </summary>
        AutoBill = 11,

        /// <summary> Виджет калькулятора Аутсорсинга </summary>
        OutCalculatorWidget = 12,

        /// <summary> Счет выставлен через движок триггеров </summary>
        TriggerEngine = 13,

        /// <summary>
        /// Счёт выставлен из мобильного приложения (для интеграции с банками)
        /// </summary>
        MobileAppToBank = 14,
    }
}
