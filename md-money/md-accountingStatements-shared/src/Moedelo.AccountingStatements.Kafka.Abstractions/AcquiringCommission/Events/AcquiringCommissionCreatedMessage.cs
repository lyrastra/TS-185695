using Moedelo.AccountingStatements.Enums;
using System;
using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Models;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Events
{
    /// <summary>
    /// Событие по созданию бухсправки "Комиссия за эквайринг"
    /// </summary>
    public class AcquiringCommissionCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }
        
        public Nds Nds { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// DocumentBaseId входящего платежа
        /// </summary>
        public long PaymentBaseId { get; set; }
        
        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        /// <remarks> Используется для генерации сабконто банка в бухсправке НУ </remarks>
        public int SettlementAccountId { get; set; }
    }
}