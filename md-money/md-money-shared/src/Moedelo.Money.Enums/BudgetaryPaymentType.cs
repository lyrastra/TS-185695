namespace Moedelo.Money.Enums
{
    /// <summary>
    /// Тип бюджетного платежа
    /// </summary>
    public enum BudgetaryPaymentType
    {
        /// <summary>
        /// Небюджетный платеж
        /// </summary>
        None = 0,

        /// <summary>
        /// НС - налоговый сбор
        /// </summary>
        TaxPayment = 1,

        /// <summary>
        /// ВЗ - взнос
        /// </summary>
        Fee = 2,

        /// <summary>
        /// ПЛ - уплата платежа
        /// </summary>
        Pay = 3,

        /// <summary>
        /// АВ - уплата аванса или предоплата
        /// </summary>
        Advance = 4,

        /// <summary>
        /// ГП - уплата пошлины; (При выборе прочее)
        /// </summary>
        Duty = 5,

        /// <summary>
        /// ПЕ - уплата пени; (для всех)
        /// </summary>
        Peni = 6,

        /// <summary>
        /// СА - налоговые санкции, установленные Налоговым кодексом Российской Федерации; (для ИФНС и и прочего)
        /// </summary>
        TaxSanction = 7,

        /// <summary>
        /// АШ - административные штрафы; (для всех)
        /// </summary>
        AdministrationPenalty = 8,

        /// <summary>
        /// ИШ - иные штрафы, установленные соответствующими законодательными или иными нормативными актами. (для всех)
        /// </summary>
        AnotherPenalty = 9,

        /// <summary>
        /// ПЦ - уплата процентов; (прочие)
        /// </summary>
        Percent = 10,

        /// <summary>
        /// 0 - остальные типы платежей
        /// </summary>
        Other = 11
    }
}