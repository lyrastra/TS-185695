namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum ChargeTypeCode
    {
        /// <summary>Гражданский правовой договор  </summary>
        GPD = -2,

        /// <summary> пользовательский тип </summary>
        Custom = -1,

        /// <summary> Оклад  </summary>
        GeneralSalary = 0,

        /// <summary> Доплаты, надбавки </summary>
        Bonuses = 1000,

        /// <summary> Премии </summary>
        Premium = 2000,

        /// <summary> Материальная помощь, кажется по факту не используется, Материальная помощь идет как прочие начисление OtherIncomeType </summary>
        MaterialAid = 2700,

        /// <summary> Отпуск </summary>
        Vacation = 4000,

        /// <summary> Больничный </summary>
        SickList = 4400,

        /// <summary> Другие виды отсутствия </summary>
        OtherAbsence = 4700,

        /// <summary> Присутствия и оплата по среднему заработку </summary>
        Presence = 5000,

        /// <summary>Выплаты при увольнении</summary>
        Fired = 5100,

        /// <summary> доп.доход и прочие начисления </summary>
        OtherIncome = 6000,

        /// <summary> удержания,долги </summary>
        Deduction = 7000,

        /// <summary> Аванс  </summary>
        Advance = 9000,

        /// <summary> Налог на доход </summary>
        Tax = 10000,

        /// <summary> Техническое начисление</summary>
        Technical = 11000,
    }
}
