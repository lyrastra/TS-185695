namespace Moedelo.Finances.Enums
{
    public enum BudgetaryPeriodType
    {
        /// <summary> Не указан период </summary>
        None = 0,

        /// <summary> Год (ГД) </summary>
        Year,

        /// <summary> Полугодие (ПЛ) </summary>
        HalfYear,

        /// <summary> Квартал (КВ) </summary>
        Quarter,

        /// <summary> Месяц (МС) </summary>
        Month,

        /// <summary> Первая декада месяца </summary>
        Decade1,

        /// <summary> Вторая декада месяца </summary>
        Decade2,

        /// <summary> Третья декада месяца </summary>
        Decade3,

        /// <summary> 0 - Без периода </summary>
        NoPeriod,

        /// <summary> Указана конкретная дата </summary>
        Date
    }
}