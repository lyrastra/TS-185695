namespace Moedelo.Money.Enums.Outsource
{
    public enum OutsourceConfirmPaymentStatus
    {
        /// <summary>
        /// Нет ошибок
        /// </summary>
        Ok = 0,

        /// <summary>
        /// Платеж не найден
        /// </summary>
        NotFound = 1,

        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        Error = 2,

        /// <summary>
        /// Платеж в закрытом периоде
        /// </summary>
        ClosedPeriod = 3,
        
        /// <summary>
        /// Платеж сохранен в красную таблицу
        /// </summary>
        BadState = 4,
    }
}