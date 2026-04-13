namespace Moedelo.Payroll.Enums.PaymentEvents
{
    public enum PaymentEventType
    {
        /// <summary>
        /// Перечисление зарплаты
        /// </summary>
        Salary = 0,

        /// <summary>
        /// Аванс
        /// </summary>
        Advance = 1,

        /// <summary> 
        /// Отпускные 
        /// </summary>
        Vacation = 2,

        /// <summary> 
        /// Расчеты с фондами
        /// </summary>
        Funds = 3,

        /// <summary> 
        /// ГПД 
        /// </summary>
        Contract = 4,

        /// <summary> 
        /// Дивиденды 
        /// </summary>
        Dividends = 5,

        /// <summary> 
        /// Пользовательский тип 
        /// </summary>
        Custom = 6,
        
        /// <summary> 
        /// Премии 
        /// </summary>
        Premium = 7,

        /// <summary> 
        /// Единовремменные выплаты пособий 
        /// </summary>
        OneTimeAllowance = 8,

        /// <summary> 
        /// Командировочные 
        /// </summary>
        BusinessTripExpenses = 9,

        /// <summary>
        /// Выплаты при увольнении
        /// </summary>
        Fired = 10,

        /// <summary> 
        /// Прочие начисления 
        /// </summary>
        OtherIncome = 11,

        /// <summary> 
        /// Больничные
        /// </summary>
        SickList = 12,

        /// <summary>
        /// Выплаты присутствий и оплат по среднему заработку
        /// </summary>
        Presence = 13,

        /// <summary>
        /// Пособия по уходу за ребенком
        /// </summary>
        AllowanceForChild = 14,

        /// <summary>
        /// Перечисление зарплаты при увольнении
        /// </summary>
        FiredSalary = 15
    }
}