using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands
{
    public class ImportDuplicateRefundToCustomer : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long ContractBaseId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        

        /// <summary>
        /// Правило импорта применённое к операции
        /// </summary>
        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
