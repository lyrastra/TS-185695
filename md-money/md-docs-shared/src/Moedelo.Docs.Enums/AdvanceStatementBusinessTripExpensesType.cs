namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Тип командировочных расходов
    /// </summary>
    public enum BusinessTripExpensesType
    {
        /// <summary>
        /// Значение не определено
        /// </summary>
        Default = 0,

        /// <summary>
        /// Проезд
        /// </summary>
        Passage = 1,

        /// <summary>
        /// Проживание
        /// </summary>
        Living = 2,

        /// <summary>
        /// Дополнительные расходы
        /// </summary>
        Additional = 3,

        /// <summary>
        /// Иной транспорт
        /// </summary>
        OtherTransport = 4,

        /// <summary>
        /// Без документов
        /// </summary>
        WithoutDocs = 5,

        /// <summary>
        /// Суточные
        /// </summary>
        DailySum = 6
    }
}
