using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands
{
    public class ImportCurrencyPaymentToSupplier : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long? ContractBaseId { get; set; }

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
