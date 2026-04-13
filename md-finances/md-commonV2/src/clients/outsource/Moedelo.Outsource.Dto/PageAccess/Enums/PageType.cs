namespace Moedelo.Outsource.Dto.PageAccess.Enums
{
    public enum PageType
    {
        /// <summary>
        /// Массовая работа с требованиями - требования
        /// </summary>
        ErptDemand = 0,

        /// <summary>
        /// Массовая работа с требованиями - письма
        /// </summary>
        ErptLetter = 1,

        /// <summary>
        /// Зарплата - Автопрохождение з/п и ЕНП
        /// </summary>
        PayrollEnp = 2,

        /// <summary>
        /// Зарплата - Автопрохождение ЕФС-1 (кадры)
        /// </summary>
        PayrollEfs = 3,

        /// <summary>
        /// Массовый анализ учета - бухгалтерские проверки
        /// </summary>
        ClientAnalyticsAccounting = 4,

        /// <summary>
        /// Массовый анализ учета - зарплатные проверки
        /// </summary>
        ClientAnalyticsPayroll = 5,
    }
}