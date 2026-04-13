namespace Moedelo.Money.Enums
{
    /// <summary>
    /// Тип периода
    /// </summary>
    public enum BudgetaryPeriodType
    {
        /// <summary> 
        /// Не указан период 
        /// </summary>
        None = 0,

        /// <summary>
        /// Год (ГД)
        /// </summary>
        Year = 1,

        /// <summary>
        /// Полугодие (ПЛ)
        /// </summary>
        HalfYear = 2,

        /// <summary>
        /// Квартал (КВ)
        /// </summary>
        Quarter = 3,

        /// <summary>
        /// Месяц (МС)
        /// </summary>
        Month = 4,

        /// <summary>
        /// 0 - Без периода
        /// </summary>
        NoPeriod = 8,

        /// <summary>
        /// Указана конкретная дата
        /// </summary>
        Date = 9
    }
}
