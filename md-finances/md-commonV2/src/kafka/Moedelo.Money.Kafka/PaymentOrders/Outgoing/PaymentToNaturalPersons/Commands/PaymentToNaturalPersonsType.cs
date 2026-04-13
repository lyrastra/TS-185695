namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands
{
    public enum PaymentToNaturalPersonsType
    {
        /// <summary>
        /// Выплаты по трудовому договору
        /// </summary>
        WorkContract = 1,

        /// <summary>
        /// Выплаты по ГПД
        /// </summary>
        Gpd = 2,

        /// <summary>
        /// Выплата дивидендов
        /// </summary>
        Dividends = 3,

        /// <summary>
        /// Выплата по зарплатному проекту
        /// </summary>
        SalaryProject = 4,

        /// <summary>
        /// Выплаты ГПД по зарплатному проекту
        /// </summary>
        GpdBySalaryProject = 5,

        /// <summary>
        /// Выплата дивидендов по зарплатному проекту
        /// </summary>
        DividendsBySalaryProject = 6
    }
}
