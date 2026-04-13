using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands
{
    public class ImportAccrualOfInterest : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Правило импорта применённое к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
