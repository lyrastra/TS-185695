using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using System;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events
{
    public class WithholdingOfFeeUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int PurseId { get; set; }

        public decimal Sum { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Выбранная СНО в операции
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Тип операции, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }
        
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }
    }
}
