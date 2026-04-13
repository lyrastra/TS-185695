namespace Moedelo.Payroll.Shared.Enums.Charge
{
    public enum ChargeTypeCode
    {
        /// <summary> Оклад  </summary>
        GeneralSalary = 100,

        /// <summary> Доплаты, надбавки </summary>
        Bonuses = 200,

        /// <summary> Премии </summary>
        Premium = 300,

        /// <summary> Материальная помощь, кажется по факту не используется, Материальная помощь идет как прочие начисление OtherIncomeType </summary>
        MaterialAid = 400,

        /// <summary> Отпуск </summary>
        Vacation = 500,

        /// <summary> Больничный </summary>
        SickList = 600,

        /// <summary> Другие виды отсутствия </summary>
        OtherAbsence = 700,

        /// <summary> Присутствия и оплата по среднему заработку </summary>
        Presence = 800,

        /// <summary>Выплаты при увольнении</summary>
        Fired = 900,

        /// <summary> доп.доход и прочие начисления </summary>
        OtherIncome = 1000,

        /// <summary> удержания,долги </summary>
        Deduction = 1100,

        /// <summary> Аванс  </summary>
        Advance = 1200,

        /// <summary> Налог на доход </summary>
        Tax = 1300,

        /// <summary>Гражданский правовой договор  </summary>
        WorkContract = 1400,

        /// <summary> пользовательский тип </summary>
        Custom = 1500,
        
        /// <summary> прогул </summary>
        Truancy = 1600
    }
}