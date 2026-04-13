using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Commands
{
    public class ImportRetailRevenue : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Комиссия (эквайринг)
        /// </summary>
        public decimal? AcquiringCommissionSum { get; set; }

        /// <summary>
        /// Дата комиссии
        /// </summary>
        public DateTime? AcquiringCommissionDate { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }
        
        public Nds Nds { get; set; }

        /// <summary>
        /// Правила импорта применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Признак: посредничество
        /// </summary>
        public bool IsMediation { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
