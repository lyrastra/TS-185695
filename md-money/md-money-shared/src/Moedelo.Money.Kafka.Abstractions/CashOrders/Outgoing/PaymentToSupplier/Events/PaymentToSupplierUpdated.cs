using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier.Events
{
    public class PaymentToSupplierUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public ContractorBase Contractor { get; set; }

        public bool IsMainContractor { get; set; }

        public long? ContractBaseId { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Тип кассового ордера, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }
    }
}
