namespace Moedelo.Common.Enums.Enums.Tools
{
    /// <summary>
    /// Типы инструментов
    /// </summary>
    public enum ToolType
    {
        /// <summary>
        /// Выездные налоговые проверки.
        /// </summary>
        UnannouncedInspection = 1,

        /// <summary>
        /// Плановые проверки
        /// </summary>
        ScheduledInspection = 2,

        /// <summary>
        /// Проверка по ФНС
        /// </summary>
        FnsCheck = 3,

        /// <summary>
        /// Проверка по арбитражным судам
        /// </summary>
        ArbitrationCheck = 4,

        /// <summary>
        /// Проверка контрагента по многим сервисам одновременно
        /// </summary>
        KontragentCheck = 5,

        /// <summary>
        /// Рассчет НДС
        /// </summary>
        NdsCheck = 6,

        /// <summary>
        /// Калькуляторы
        /// </summary>
        Calculator = 7,

        /// <summary>
        /// Скачивание архива по арбитражам
        /// </summary>
        DownloadArbitrationArchive = 8,

        /// <summary>
        /// Связи
        /// </summary>
        Relations = 9,

        /// <summary>
        /// Проверка контрагента по многим сервисам одновременно
        /// </summary>
        KontragentMassCheck = 10,

        /// <summary>
        /// Карта связей
        /// </summary>
        RelationsMap = 11,
        
        /// <summary>
        /// Создание дочерней организации
        /// </summary>
        AffiliatedCompanyCreation = 12,
        
        /// <summary>
        /// Количество одновременных сессий пользователя
        /// </summary>
        Session = 13,
        
        /// <summary>
        /// Работник
        /// </summary>
        Worker = 14,

        /// <summary>
        /// Длительность триального периода в днях
        /// </summary>
        TrialDaysCount = 15,

        /// <summary>
        /// Количество банковских интеграций
        /// </summary>
        BankIntegrationsCount = 16,

        /// <summary>
        /// Количество дополнительных пользователей сервиса (помимо одного основнного)
        /// </summary>
        AccountExtraUsersCount = 17,
        
        /// <summary>
        /// Количество складов
        /// </summary>
        StocksCount = 18,

        /// <summary>
        /// Количество вопросов в эксперт онлайн
        /// </summary>
        NumberQuestionsInExpertOnline = 19,

        /// <summary>
        /// Количество вопросов в консалтинг - Бухгалтерия
        /// </summary>
        NumberQuestionsInConsultingAccounting = 20,

        /// <summary>
        /// Количество вопросов в консалтинг - Кадры
        /// </summary>
        NumberQuestionsInConsultingPersonnel = 21,

        /// <summary>
        /// Количество вопросов в юридический консалтинг
        /// </summary>
        NumberQuestionsInLegalConsulting = 22,

        /// <summary>
        /// Количество вопросов в консалтинг - Бухгалтерия или Кадры
        /// </summary>
        NumberQuestionsInLegalAccountingAndPersonnel = 23,

        /// <summary>
        /// Количество запросов в историю изменений контрагентов
        /// </summary>
        ChangeOfHistory = 24,

        /// <summary>
        /// Количество запросов в подбор контрагентов
        /// </summary>
        SelectionOfContractors = 25,

        /// <summary>
        /// Количество запросов на проверку физ. лиц
        /// </summary>
        VerificationOfIndividuals = 26,

        /// <summary>
        /// Обороты
        /// </summary>
        MoneyTurnover = 27,

        /// <summary>
        /// Количество сотрудников на обслуживании
        /// </summary>
        NumberEmployeesInService = 28,
        
        /// <summary>
        /// Общий лимит для всех консультаций
        /// </summary>
        NumberQuestionsInAllConsultations = 29,
    }
    
    ///////////  ВНИМАНИЕ  ///////////
    // добавляя значение сюда, обязательно добавь его в
    // https://github.com/moedelo/md-authorization/blob/master/src/apps/Moedelo.Authorization.Domain/Legacy/ToolType.cs
    // а также добавь соответствующий маппинг в md-authorization
    //////////////////////////////////
}