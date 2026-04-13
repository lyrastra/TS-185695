using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Exceptions
{
    /// <summary>
    /// Запрашиваемый тип п/п не совпадает с фактичеким 
    /// </summary>
    public class CashOrderMismatchTypeExcepton : Exception
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Ожидаемый тип
        /// </summary>
        public OperationType Expected { get; set; }

        /// <summary>
        /// Фактический тип
        /// </summary>
        public OperationType Actual { get; set; }
    }
}