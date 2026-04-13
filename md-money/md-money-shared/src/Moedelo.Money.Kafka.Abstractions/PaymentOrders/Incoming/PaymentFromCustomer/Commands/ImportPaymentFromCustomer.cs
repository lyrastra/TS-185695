using System;
using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Commands
{
    public class ImportPaymentFromCustomer : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long? ContractBaseId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        /// <summary>
        /// В том числе НДС для посредничества
        /// </summary>
        public Nds MediationNds { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Вознаграждение посредника
        /// </summary>
        public decimal? MediationCommissionSum { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public IReadOnlyCollection<BillLink> BillLinks { get; set; } = Array.Empty<BillLink>();

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Правила импорта, применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
