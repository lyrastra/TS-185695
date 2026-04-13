using System.ComponentModel;
using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.FinancialOperations
{
    public enum BudgetaryPaymentSubtype
    {
        Default = 0,

        /// <summary>
        /// НС - уплата налога или сбора; (показываем для ИФНС и прочего)
        /// </summary>
        [Description("НС")]
        Ns = 1,

        /// <summary>
        /// ВЗ - уплата взноса; (Показываем для ПФР,ФСС для ИФНС и прочего)
        /// </summary>
        [Description("ВЗ")]
        Vz = 2,

        /// <summary>
        /// ГП - уплата пошлины; (При выборе прочее)
        /// </summary>
        [Description("ГП")]
        Gp = 3,

        /// <summary>
        /// ПЕ - уплата пени; (для всех)
        /// </summary>
        [Description("ПЕ")]
        [Peni]
        Pe = 4,

        /// <summary>
        /// СА - налоговые санкции, установленные Налоговым кодексом Российской Федерации; (для ИФНС и и прочего)
        /// </summary>
        [Description("СА")]
        [Penalty]
        Sa = 5,

        /// <summary>
        /// АШ - административные штрафы; (для всех)
        /// </summary>
        [Description("АШ")]
        [Penalty]
        Ash = 6,

        /// <summary>
        /// ИШ - иные штрафы, установленные соответствующими законодательными или иными нормативными актами. (для Всех)
        /// </summary>
        [Description("ИШ")]
        [Penalty]
        Ish = 7,

        /// <summary>
        /// ПЦ - уплата процентов; (прочие)
        /// </summary>
        [Description("ПЦ")]
        Pc = 8,

        /// <summary>
        /// АВ - уплата аванса или предоплата; (прочие)
        /// </summary>
        [Description("АВ")]
        Av = 9,

        /// <summary>
        /// ПЛ - уплата платежа; (прочие)
        /// </summary>
        [Description("ПЛ")]
        Pl = 10,

        /// <summary>
        /// Все остальные виды платежей
        /// </summary>
        [Description("0")]
        Other = 11
    }
}