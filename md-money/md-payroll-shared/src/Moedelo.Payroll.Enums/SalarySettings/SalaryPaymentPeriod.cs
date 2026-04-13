namespace Moedelo.Payroll.Enums.SalarySettings
{
    /// <summary>
    /// В каком месяце мы выплачиваем зарплату
    /// </summary>
    public enum SalaryPaymentPeriod
    {
        /// <summary>
        /// Выплата зарплаты производится в месяце, следующим за расчетным
        /// </summary>
        NextMonth = 0,

        /// <summary>
        /// Выплата зарплаты производится в расчетном месяце
        /// </summary>
        CurrentMonth = 1,
    }
}