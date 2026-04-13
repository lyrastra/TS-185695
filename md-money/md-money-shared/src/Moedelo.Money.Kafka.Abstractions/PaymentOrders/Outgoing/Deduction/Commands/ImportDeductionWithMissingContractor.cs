using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands
{
    public class ImportDeductionWithMissingContractor : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Правила импорта применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        public bool IsBudgetaryDebt { get; set; }
        
        public string PayerStatus { get; set; }
        
        public string Kbk { get; set; }
        
        public string Oktmo { get; set; }
        
        public string Uin { get; set; }

        public PaymentPriority PaymentPriority { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
