namespace Moedelo.Common.Enums.Enums.FinancialOperations
{
    public enum OtherTransferType
    {
        /// <summary>
        /// Прочее (выбрано по умолчанию)
        /// </summary>
        Other = 0,

        /// <summary>
        /// Погашение займа
        /// </summary>
        LoanPayment = 1,

        /// <summary>
        /// Получение процентов
        /// </summary>
        ReceivePercents = 2,

        /// <summary>
        /// Погашение займа и получение процентов
        /// </summary>
        LoanPaymentAndReceivePercents = 3,

        /// <summary>
        /// Получение процентов за депозит
        /// </summary>
        ReceivePercentsForDeposit = 4,

        /// <summary>
        /// Возврат с депозита
        /// </summary>
        ReturnFromDeposit = 5,

        /// <summary>
        /// Продажа товаров
        /// </summary>
        SaleGoods = 6,

        /// <summary>
        /// Взаимозачет
        /// </summary>
        ContraDeal = 7,

        /// <summary>
        /// Оказание услуг
        /// </summary>
        Services = 8,

        /// <summary>
        /// Выдача займа
        /// </summary>
        LoanIssue = 9,

        /// <summary>
        /// Перечисление на депозит
        /// </summary>
        PaymentToDeposit = 10,

        /// <summary>
        /// Целевое поступление
        /// </summary>
        TargetFund = 11,

        /// <summary>
        /// Курсовая разница
        /// </summary>
        ExchangeDiff = 12,

        /// <summary>
        /// По удержанию
        /// </summary>
        SalaryDeduction = 13
    }
}