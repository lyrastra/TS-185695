namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;

/// <summary>
/// Перевод платежа: модели
/// </summary>
public class TransferPayment
{
    public class RequestDto
    {
        /// <summary>
        /// Источник переноса (БИЗ): информация о фирме
        /// </summary>
        public int FromFirmId { get; set; }

        /// <summary>
        /// Цель переноса (УС): информация о фирме
        /// </summary>
        public int ToFirmId { get; set; }

        /// <summary>
        /// Объект переноса: платеж
        /// </summary>
        public int PaymentId { get; set; }
    }

    public class ResultDto
    {
        /// <summary>
        /// Соответствие платежей
        /// </summary>
        public TransferedPaymentMapDto PaymentMap { get; set; }

        /// <summary>
        /// Без ошибок ли педеведен платеж 
        /// </summary>
        public bool Success { get; set; }
    }
}