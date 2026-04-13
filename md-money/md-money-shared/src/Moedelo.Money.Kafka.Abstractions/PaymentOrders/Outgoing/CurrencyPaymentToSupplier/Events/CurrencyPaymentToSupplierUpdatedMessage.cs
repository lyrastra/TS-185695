using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events
{
    public class CurrencyPaymentToSupplierUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Сумма в валюте
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Сумма в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;
        
        public long? ContractBaseId { get; set; }

        /// <summary>
        /// Новые связанные инвойсы
        /// </summary>
        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();
        
        /// <summary>
        /// Старые связанные инвойсы
        /// </summary>
        public IReadOnlyCollection<DocumentLink> OldDocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}