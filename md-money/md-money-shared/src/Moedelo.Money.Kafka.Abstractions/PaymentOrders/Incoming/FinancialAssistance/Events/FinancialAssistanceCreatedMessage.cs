using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance.Events
{
    public class FinancialAssistanceCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public decimal Sum { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}