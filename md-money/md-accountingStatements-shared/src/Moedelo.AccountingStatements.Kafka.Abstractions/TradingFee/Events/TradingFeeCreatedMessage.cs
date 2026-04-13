using Moedelo.AccountingStatements.Enums;
using System;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee.Events
{
    /// <summary>
    /// Событие по созданию бухсправки "Торговый сбор"
    /// </summary>
    public class TradingFeeCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// DocumentBaseId входящего платежа
        /// </summary>
        public long PaymentBaseId { get; set; }
    }
}