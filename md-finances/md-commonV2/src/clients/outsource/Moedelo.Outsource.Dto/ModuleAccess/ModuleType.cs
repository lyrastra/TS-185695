namespace Moedelo.Outsource.Dto.ModuleAccess
{
    /// <summary>
    /// Типы подключаемых модулей
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// Массовая работа требования
        /// </summary>
        ErptDemands = 1,

        /// <summary>
        /// Массовая работа со складом
        /// </summary>
        Stock = 2,

        /// <summary>
        /// Массовая работа с мастером закрытия месяца
        /// </summary>
        Mzm = 3,

        /// <summary>
        /// Отдельный почтовый ящик для каждого клиента
        /// </summary>
        IndividualMailParse = 4,
        
        /// <summary>
        /// Массовая работа с криптозаданиями (центр подписания)
        /// </summary>
        EdsCryptoCenter = 5,
        
        /// <summary>
        /// Массовая работа с выписками
        /// </summary>
        PaymentImport = 6,
        
        /// <summary>
        /// Классификация документов
        /// </summary>
        DocumentsClassifier = 7,

        /// <summary>
        /// Массовый анализ учета клиентов
        /// </summary>
        ClientAnalytics = 8,

        /// <summary>
        /// Массовая работа с зарплатой
        /// </summary>
        Salary = 9,
        
        /// <summary>
        /// Автоматическая классификация задач
        /// </summary>
        TaskClassifier = 10,

    }
}