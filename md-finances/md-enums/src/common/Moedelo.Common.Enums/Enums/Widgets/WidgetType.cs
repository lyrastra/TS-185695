namespace Moedelo.Common.Enums.Enums.Widgets
{
    public enum WidgetType
    {
        Unknown = 0,

        /// <summary>
        /// Документы на продажу (вкл. счета)
        /// </summary>
        SaleDocumentsWidget = 1,

        /// <summary>
        /// Документы на покупку
        /// </summary>
        BuyDocumentsWidget = 2,

        /// <summary>
        /// Деньги (учетка)
        /// </summary>
        PaymentDocuments = 3,

        /// <summary>
        /// Налоговый календарь
        /// </summary>
        TaxCalendar = 4,

        /// <summary>
        /// Электронная отчетность
        /// </summary>
        EReport = 5,

        /// <summary>
        /// Выплаты физ.лицам, НДФЛ, взносы
        /// </summary>
        SalaryPaymentsWidget = 6,

        /// <summary>
        /// 5 топ контрагентов пользователя
        /// </summary>
        Kontragents = 7,

        /// <summary>
        /// Приведи друга
        /// </summary>
        InviteFriends = 9,

        /// <summary>
        /// Бонусная программа
        /// </summary>
        BonusProgram = 10,

        /// <summary>
        /// Чат для пользователя, находящегося под обслуживание профессионального аутсорсера
        /// </summary>
        OutsourceChat = 11,

        /// <summary>
        /// Персональный бухгалтер - запрос на платную бух. консультацию
        /// </summary>
        Consultations = 12,

        /// <summary>
        /// НДС
        /// </summary>
        Nds = 13,

        /// <summary>
        /// Налог на прибыль 
        /// </summary>
        IncomeTax = 14,

        /// <summary>
        /// Onboarding
        /// </summary>
        Onboarding = 15,
        
        /// <summary>
        /// Календарь отчетности в ГИС ЖКХ
        /// </summary>
        GisCalendar = 16,
        
        /// <summary>
        /// Виджет отчетов в Росстат
        /// </summary>
        RosStat = 17,

        /// <summary>
        /// Onboarding
        /// </summary>
        NewOnboarding = 18,

        /// <summary>
        /// Новый виджет денег с остатками
        /// </summary>
        MoneyWithBalances = 19
    }
}