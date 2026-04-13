using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Commands
{
    public class ImportRefundToSettlementAccount : IEntityCommandData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Правила импорта применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
