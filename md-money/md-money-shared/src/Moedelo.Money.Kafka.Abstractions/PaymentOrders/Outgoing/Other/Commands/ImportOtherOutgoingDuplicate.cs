using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands
{
    /// <summary>
    /// Как ImportOther, но для дубликата
    /// </summary>
    public class ImportOtherOutgoingDuplicate : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }
        
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Правила импорта применённые к операции
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }

        /// <summary>
        /// Бухгалтерская проводка, она создаётся в импортированном платеже
        /// </summary>
        public OutgoingOtherAccPosting AccPosting { get; set; }
        /// <summary>
        /// Налоговая проводка
        /// </summary>
        /// <remarks>
        /// Операции из "красной таблицы" не должны проводиться в БУ/НУ, но пусть это решит обработчик команды
        /// </remarks>
        public CustomTaxPosting TaxPosting { get; set; }
    }
}
