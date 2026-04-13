namespace Moedelo.Payroll.Enums.ProductionCalendars
{
    public enum ProductionCalendarDayType
    {
        /// <summary>
        /// Не определен
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Стандартный рабочий день
        /// </summary>
        Standart = 0,

        /// <summary>
        /// Выходной
        /// </summary>
        Weekend = 1,

        /// <summary>
        /// Праздничный день
        /// </summary>
        Holiday = 2,

        /// <summary>
        /// Сокращенный рабочий день
        /// </summary>
        ShortDay = 3,

        /// <summary> Нерабочий день с сохранением зарплаты </summary>
        PaidHoliday = 4
    }
}