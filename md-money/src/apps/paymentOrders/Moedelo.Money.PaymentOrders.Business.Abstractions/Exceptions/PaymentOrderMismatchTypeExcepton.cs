using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions
{
    /// <summary>
    /// Запрашиваемый тип п/п не совпадает с фактичеким 
    /// </summary>
    public class PaymentOrderMismatchTypeExcepton : Exception
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Тип по текущему процессу обработки
        /// </summary>
        public OperationType ExpectedType { get; set; }

        /// <summary>
        /// Тип который сейчас сохранен в базе
        /// </summary>
        public OperationType ActualType { get; set; }
    }
}