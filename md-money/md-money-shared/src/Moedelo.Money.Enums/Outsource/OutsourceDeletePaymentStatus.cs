namespace Moedelo.Money.Enums.Outsource
{
    public enum OutsourceDeletePaymentStatus
    {
        /// <summary>
        /// Нет ошибок
        /// </summary>
        Ok = 0,

        /// <summary>
        /// Нет ошибок
        /// </summary>
        NotFound = 1,

        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        Error = 2,

        /// <summary>
        /// Операция в закрытом периоде
        /// </summary>
        ClosedPeriod = 3,
    }
}