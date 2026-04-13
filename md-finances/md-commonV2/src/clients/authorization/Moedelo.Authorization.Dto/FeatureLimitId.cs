namespace Moedelo.Authorization.Dto;

/// <summary>
/// перечень функциональных лимитов, которые могут быть установлены для фирмы
/// </summary>
public enum FeatureLimitId
{
    #region "Moedelo.Common.Enums.Enums.Tools.ToolType"
    /// <summary>
    /// Выездные налоговые проверки.
    /// </summary>
    UnannouncedInspection = 101,

    /// <summary>
    /// Плановые проверки
    /// </summary>
    ScheduledInspection = 102,

    /// <summary>
    /// Проверка по ФНС
    /// </summary>
    FnsCheck = 103,

    /// <summary>
    /// Проверка по арбитражным судам
    /// </summary>
    ArbitrationCheck = 104,

    /// <summary>
    /// Проверка контрагента по многим сервисам одновременно
    /// </summary>
    KontragentCheck = 105,

    /// <summary>
    /// Рассчет НДС
    /// </summary>
    NdsCheck = 106,

    /// <summary>
    /// Калькуляторы
    /// </summary>
    Calculator = 107,

    /// <summary>
    /// Скачивание архива по арбитражам
    /// </summary>
    DownloadArbitrationArchive = 108,

    /// <summary>
    /// Связи
    /// </summary>
    Relations = 109,

    /// <summary>
    /// Проверка контрагента по многим сервисам одновременно
    /// </summary>
    KontragentMassCheck = 110,

    /// <summary>
    /// Карта связей
    /// </summary>
    RelationsMap = 111,
        
    /// <summary>
    /// Создание дочерней организации
    /// </summary>
    AffiliatedCompanyCreation = 112,
        
    /// <summary>
    /// Количество одновременных сессий пользователя
    /// </summary>
    Session = 113,
        
    /// <summary>
    /// Работник
    /// </summary>
    Worker = 114,

    /// <summary>
    /// Длительность триального периода в днях
    /// </summary>
    TrialDaysCount = 115,

    /// <summary>
    /// Количество банковских интеграций
    /// </summary>
    BankIntegrationsCount = 116,

    /// <summary>
    /// Количество дополнительных пользователей сервиса (помимо одного основнного)
    /// </summary>
    AccountExtraUsersCount = 117,
        
    /// <summary>
    /// Количество складов
    /// </summary>
    StocksCount = 118,

    /// <summary>
    /// Количество вопросов в эксперт онлайн
    /// </summary>
    NumberQuestionsInExpertOnline = 119,

    /// <summary>
    /// Количество вопросов в консалтинг - Бухгалтерия
    /// </summary>
    NumberQuestionsInConsultingAccounting = 120,

    /// <summary>
    /// Количество вопросов в консалтинг - Кадры
    /// </summary>
    NumberQuestionsInConsultingPersonnel = 121,

    /// <summary>
    /// Количество вопросов в юридический консалтинг
    /// </summary>
    NumberQuestionsInLegalConsulting = 122,

    /// <summary>
    /// Количество вопросов в консалтинг - Бухгалтерия или Кадры
    /// </summary>
    NumberQuestionsInLegalAccountingAndPersonnel = 123,

    /// <summary>
    /// Количество запросов в историю изменений контрагентов
    /// </summary>
    ChangeOfHistory = 124,

    /// <summary>
    /// Количество запросов в подбор контрагентов
    /// </summary>
    SelectionOfContractors = 125,

    /// <summary>
    /// Количество запросов на проверку физ. лиц
    /// </summary>
    VerificationOfIndividuals = 126,

    /// <summary>
    /// Обороты
    /// </summary>
    MoneyTurnover = 127,

    /// <summary>
    /// Количество сотрудников на обслуживании
    /// </summary>
    NumberEmployeesInService = 128,

    #endregion

    /// <summary>
    /// Количество автооплачиваемых связанных организаций
    /// </summary>
    AutoPaidAffiliatedFirmCount = 219,

    /// <summary>
    /// Количество сотрудников для корпоративного электронного документооборота
    /// </summary>
    NumberEmployeesForKedo = 220,

    /// <summary>
    /// Количество многоквартирных домов на обслуживании
    /// </summary>
    NumberBuildingsOnService = 221,

    /// <summary>
    /// Количество квартир на обслуживании
    /// </summary>
    NumberApartmentsOnService = 222,

    /// <summary>
    /// Общий лимит для всех консультаций
    /// </summary>
    NumberQuestionsInAllConsultations = 223
}