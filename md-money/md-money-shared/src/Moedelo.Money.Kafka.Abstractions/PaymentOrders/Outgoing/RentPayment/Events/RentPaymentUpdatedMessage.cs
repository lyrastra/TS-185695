using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events
{
    public class RentPaymentUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long? ContractBaseId { get; set; }

        public long? InventoryCardBaseId { get; set; }

        public IReadOnlyCollection<RentPeriod> RentPeriods { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
