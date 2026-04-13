using System.ComponentModel;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// Тип выплаты физ.лицам
    /// </summary>
    public enum PaymentToNaturalPersonsType
    {
        /// <summary>
        /// Выплаты по трудовому договору
        /// </summary>
        [Description("Выплаты по трудовому договору")]
        WorkContract = 1,

        /// <summary>
        /// Выплаты по ГПД
        /// </summary>
        [Description("Выплаты по ГПД")]
        Gpd = 2,

        /// <summary>
        /// Выплата дивидендов
        /// </summary>
        [Description("Выплата дивидендов")]
        Dividends = 3,

        /// <summary>
        /// Выплата по зарплатному проекту
        /// </summary>
        [Description("Выплата по зарплатному проекту")]
        SalaryProject = 4,

        /// <summary>
        /// Выплаты ГПД по зарплатному проекту
        /// </summary>
        [Description("Выплаты ГПД по зарплатному проекту")]
        GpdBySalaryProject = 5,

        /// <summary>
        /// Выплата дивидендов по зарплатному проекту
        /// </summary>
        [Description("Выплата дивидендов по зарплатному проекту")]
        DividendsBySalaryProject = 6
    }
}
